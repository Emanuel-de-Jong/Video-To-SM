using FFmpeg.AutoGen.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VideoToSM.VideoDecoder;

public sealed unsafe class VideoStreamDecoder : IDisposable
{
    private readonly AVCodecContext* _pCodecContext;
    private readonly AVFormatContext* _pFormatContext;
    private readonly AVFrame* _pFrame;
    private readonly AVPacket* _pPacket;
    private readonly AVFrame* _receivedFrame;
    private readonly int _streamIndex;

    public VideoStreamDecoder(string url)
    {
        _pFormatContext = ffmpeg.avformat_alloc_context();
        _receivedFrame = ffmpeg.av_frame_alloc();
        AVFormatContext* pFormatContext = _pFormatContext;
        ThrowExceptionIfError(ffmpeg.avformat_open_input(&pFormatContext, url, null, null));
        ThrowExceptionIfError(ffmpeg.avformat_find_stream_info(_pFormatContext, null));
        AVCodec* codec = null;
        ThrowExceptionIfError(_streamIndex = ffmpeg
            .av_find_best_stream(_pFormatContext, AVMediaType.AVMEDIA_TYPE_VIDEO, -1, -1, &codec, 0));
        _pCodecContext = ffmpeg.avcodec_alloc_context3(codec);

        ThrowExceptionIfError(ffmpeg.avcodec_parameters_to_context(_pCodecContext, _pFormatContext->streams[_streamIndex]->codecpar));
        ThrowExceptionIfError(ffmpeg.avcodec_open2(_pCodecContext, codec, null));

        CodecName = ffmpeg.avcodec_get_name(codec->id);
        FrameSize = new Size(_pCodecContext->width, _pCodecContext->height);
        PixelFormat = _pCodecContext->pix_fmt;

        _pPacket = ffmpeg.av_packet_alloc();
        _pFrame = ffmpeg.av_frame_alloc();

        G.ScreenWidth = _pCodecContext->width;
        G.ScreenHeight = _pCodecContext->height;

        for (int i = 0; i < pFormatContext->nb_streams; i++)
        {
            if (pFormatContext->streams[i]->codecpar->codec_type == AVMediaType.AVMEDIA_TYPE_VIDEO)
            {
                AVRational* videoStream = &pFormatContext->streams[i]->avg_frame_rate;

                G.FPS = ffmpeg.av_q2d(*videoStream);

                break;
            }
        }
    }


    public static unsafe string av_strerror(int error)
    {
        int bufferSize = 1024;
        byte* buffer = stackalloc byte[bufferSize];
        ffmpeg.av_strerror(error, buffer, (ulong)bufferSize);
        string? message = Marshal.PtrToStringAnsi((IntPtr)buffer);
        return message;
    }

    public static int ThrowExceptionIfError(int error)
    {
        return error < 0 ? throw new ApplicationException(av_strerror(error)) : error;
    }

    public string CodecName { get; }
    public Size FrameSize { get; }
    public AVPixelFormat PixelFormat { get; }

    public void Dispose()
    {
        AVFrame* pFrame = _pFrame;
        ffmpeg.av_frame_free(&pFrame);

        AVPacket* pPacket = _pPacket;
        ffmpeg.av_packet_free(&pPacket);

        ffmpeg.avcodec_close(_pCodecContext);
        AVFormatContext* pFormatContext = _pFormatContext;
        ffmpeg.avformat_close_input(&pFormatContext);
    }

    public bool TryDecodeNextFrame(out AVFrame frame)
    {
        ffmpeg.av_frame_unref(_pFrame);
        ffmpeg.av_frame_unref(_receivedFrame);
        int error;

        do
        {
            try
            {
                do
                {
                    ffmpeg.av_packet_unref(_pPacket);
                    error = ffmpeg.av_read_frame(_pFormatContext, _pPacket);

                    if (error == ffmpeg.AVERROR_EOF)
                    {
                        frame = *_pFrame;
                        return false;
                    }

                    ThrowExceptionIfError(error);
                } while (_pPacket->stream_index != _streamIndex);

                ThrowExceptionIfError(ffmpeg.avcodec_send_packet(_pCodecContext, _pPacket));
            }
            finally
            {
                ffmpeg.av_packet_unref(_pPacket);
            }

            error = ffmpeg.avcodec_receive_frame(_pCodecContext, _pFrame);
        } while (error == ffmpeg.AVERROR(ffmpeg.EAGAIN));

        ThrowExceptionIfError(error);

        if (_pCodecContext->hw_device_ctx != null)
        {
            ThrowExceptionIfError(ffmpeg.av_hwframe_transfer_data(_receivedFrame, _pFrame, 0));
            frame = *_receivedFrame;
        }
        else
        {
            frame = *_pFrame;
        }

        return true;
    }

    public IReadOnlyDictionary<string, string> GetContextInfo()
    {
        AVDictionaryEntry* tag = null;
        Dictionary<string, string> result = new();

        while ((tag = ffmpeg.av_dict_get(_pFormatContext->metadata, "", tag, ffmpeg.AV_DICT_IGNORE_SUFFIX)) != null)
        {
            string? key = Marshal.PtrToStringAnsi((IntPtr)tag->key);
            string? value = Marshal.PtrToStringAnsi((IntPtr)tag->value);
            result.Add(key, value);
        }

        return result;
    }
}
