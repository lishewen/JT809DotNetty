﻿using JT809.DotNetty.Core.Configurations;
using JT809.DotNetty.Core.Interfaces;
using JT809.DotNetty.Core.Metadata;
using JT809.DotNetty.Core.Session;
using JT809.Protocol;
using JT809.Protocol.Enums;
using JT809.Protocol.Extensions;
using JT809.Protocol.MessageBody;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.DotNetty.Core.Internal
{
    class JT809SubordinateLinkNotifyImplService : IJT809SubordinateLinkNotifyService
    {
        private readonly JT809Configuration configuration;
        private readonly JT809SuperiorMainSessionManager jT809SuperiorMainSessionManager;
        private readonly ILogger logger;
        private readonly JT809Serializer serializer;
        private readonly IJT809Config jT809Config;
        public JT809SubordinateLinkNotifyImplService(
            ILoggerFactory loggerFactory,
            IOptions<JT809Configuration> jT809ConfigurationAccessor,
            JT809SuperiorMainSessionManager jT809SuperiorMainSessionManager,
            JT809Serializer serializer,
            IJT809Config config)
        {
            this.logger = loggerFactory.CreateLogger<JT809SubordinateLinkNotifyImplService>();
            configuration = jT809ConfigurationAccessor.Value;
            this.jT809SuperiorMainSessionManager = jT809SuperiorMainSessionManager;
            this.serializer = serializer;
            jT809Config = config;
        }

        public void Notify(JT809_0x9007_ReasonCode reasonCode)
        {
            Notify(reasonCode, jT809Config.HeaderOptions.MsgGNSSCENTERID);
        }

        public void Notify(JT809_0x9007_ReasonCode reasonCode, uint msgGNSSCENTERID)
        {
            if (configuration.SubordinateClientEnable)
            {
                var session = jT809SuperiorMainSessionManager.GetSession(jT809Config.HeaderOptions.MsgGNSSCENTERID);
                if (session != null)
                {
                    //发送从链路注销请求
                    var package = JT809BusinessType.从链路断开通知消息.Create(new JT809_0x9007()
                    {
                        ReasonCode = reasonCode
                    });
                    JT809Response jT809Response = new JT809Response(package, 100);
                    if (logger.IsEnabled(LogLevel.Information))
                        logger.LogInformation($"从链路断开通知消息>>>{serializer.Serialize(package, 100).ToHexString()}");
                    session.Channel.WriteAndFlushAsync(jT809Response);
                }
            }
        }
    }
}
