﻿using DotNetty.Buffers;
using DotNetty.Codecs;
using System.Collections.Generic;
using DotNetty.Transport.Channels;
using JT809.Protocol;
using JT809.DotNetty.Core.Metadata;
using Microsoft.Extensions.Logging;

namespace JT809.DotNetty.Core.Codecs
{
    /// <summary>
    /// JT809编码
    /// </summary>
    internal class JT809Encoder : MessageToByteEncoder<JT809Response>
    {
        private readonly ILogger<JT809Encoder> logger;
        private readonly JT809Serializer serializer;

        public JT809Encoder(ILoggerFactory loggerFactory, JT809Serializer serializer)
        {
            logger = loggerFactory.CreateLogger<JT809Encoder>();
            this.serializer = serializer;
        }
        protected override void Encode(IChannelHandlerContext context, JT809Response message, IByteBuffer output)
        {
            if (message.Package != null)
            {
                var sendData = serializer.Serialize(message.Package, message.MinBufferSize);
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.LogTrace(ByteBufferUtil.HexDump(sendData));
                }
                output.WriteBytes(sendData);
            }
            else if (message.HexData != null)
            {
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.LogTrace(ByteBufferUtil.HexDump(message.HexData));
                }
                output.WriteBytes(message.HexData);
            }
        }
    }
}
