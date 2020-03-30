using JT809.DotNetty.Core.Clients;
using JT809.DotNetty.Core.Metadata;
using JT809.Protocol.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JT809.Protocol.SubMessageBody;
using JT809.Protocol.Metadata;
using JT809.Protocol.MessageBody;
using JT809.Protocol.Enums;
using JT809.Protocol;

namespace JT809.Inferior.Client
{
    public class JT809InferiorService : IHostedService
    {
        private readonly JT809MainClient mainClient;
        private readonly ILogger<JT809InferiorService> logger;
        private readonly JT809Header header;
        public JT809InferiorService(
            ILoggerFactory loggerFactory,
            JT809MainClient mainClient,
            IJT809Config config)
        {
            this.mainClient = mainClient;
            logger = loggerFactory.CreateLogger<JT809InferiorService>();
            header = new JT809Header
            {
                MsgGNSSCENTERID = config.HeaderOptions.MsgGNSSCENTERID
            };
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //5B0000001F0000053B100201341725010000000000270F00000004E8A6F25D
            var connect = mainClient.Login("218.5.80.6", 9045, new JT809_0x1001
            {
                DownLinkIP = "124.227.230.35",
                DownLinkPort = 1809,
                MsgGNSSCENTERID = 10004,
                UserId = 10004,
                Password = "12345"
            }).Result;
            if (connect)
            {
                //1301
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1300 jT809_0X1300 = new JT809_0x1300
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.平台查岗应答.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.平台查岗应答.Create_平台查岗应答(
                                new JT809_0x1300_0x1301
                                {
                                    ObjectID = "10004",
                                    InfoContent = "10004",
                                    InfoID = 10004,
                                    ObjectType = JT809_0x1301_ObjectType.当前连接的下级平台
                                })
                        };
                        var package = JT809BusinessType.主链路平台间信息交互消息.Create(header, jT809_0X1300);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1302
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1300 jT809_0X1300 = new JT809_0x1300
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.下发平台间报文应答.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.下发平台间报文应答.Create_下发平台间报文应答(
                                new JT809_0x1300_0x1302
                                {
                                    InfoID = 1234
                                })
                        };
                        var package = JT809BusinessType.主链路平台间信息交互消息.Create(header, jT809_0X1300);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1401
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1400 jT809_0X1400 = new JT809_0x1400
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.报警督办应答.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.报警督办应答.Create_报警督办应答(
                                new JT809_0x1400_0x1401
                                {
                                    SupervisionID = 10004,
                                    Result = JT809_0x1401_Result.处理中
                                })
                        };
                        var package = JT809BusinessType.主链路平台间信息交互消息.Create(header, jT809_0X1400);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1402
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1400 jT809_0X1400 = new JT809_0x1400
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.上报报警信息.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.上报报警信息.Create_上报报警信息(
                                new JT809_0x1400_0x1402
                                {
                                    WarnSrc = JT809WarnSrc.车载终端,
                                    WarnType = JT809WarnType.偏离路线报警,
                                    WarnTime = DateTime.Now,
                                    InfoContent = "Test",
                                    InfoID = 3388,
                                })
                        };
                        var package = JT809BusinessType.主链路平台间信息交互消息.Create(header, jT809_0X1400);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1403
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1400 jT809_0X1400 = new JT809_0x1400
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.主动上报报警处理结果信息.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.主动上报报警处理结果信息.Create_主动上报报警处理结果信息(
                                new JT809_0x1400_0x1403
                                {
                                    Result = JT809_0x1403_Result.将来处理,
                                    InfoID = 3388,
                                })
                        };
                        var package = JT809BusinessType.主链路平台间信息交互消息.Create(header, jT809_0X1400);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1501
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1500 jT809_0X1500 = new JT809_0x1500
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.车辆单向监听应答.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.车辆单向监听应答.Create_车辆单向监听应答(
                                new JT809_0x1500_0x1501
                                {
                                    Result = JT809_0x1501_Result.监听成功
                                })
                        };
                        var package = JT809BusinessType.主链路车辆监管消息.Create(header, jT809_0X1500);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1502
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1500 jT809_0X1500 = new JT809_0x1500
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.车辆拍照应答.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.车辆拍照应答.Create_车辆拍照应答(
                                new JT809_0x1500_0x1502
                                {
                                    PhotoRspFlag = JT809_0x1502_PhotoRspFlag.完成拍照,
                                    VehiclePosition = new JT809VehiclePositionProperties
                                    {
                                        Encrypt = JT809_VehiclePositionEncrypt.未加密,
                                        Day = 19,
                                        Month = 7,
                                        Year = 2012,
                                        Hour = 15,
                                        Minute = 15,
                                        Second = 15,
                                        Lon = 133123456,
                                        Lat = 24123456,
                                        Vec1 = 53,
                                        Vec2 = 45,
                                        Vec3 = 1234,
                                        Direction = 45,
                                        Altitude = 45,
                                        State = 1,
                                        Alarm = 1
                                    },
                                    LensID = 123,
                                    SizeType = 1,
                                    Type = 1,
                                })
                        };
                        var package = JT809BusinessType.主链路车辆监管消息.Create(header, jT809_0X1500);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1503
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1500 jT809_0X1500 = new JT809_0x1500
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.下发车辆报文应答.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.下发车辆报文应答.Create_下发车辆报文应答(
                                new JT809_0x1500_0x1503
                                {
                                    MsgID = 9999,
                                    Result = JT809_0x1503_Result.下发成功
                                })
                        };
                        var package = JT809BusinessType.主链路车辆监管消息.Create(header, jT809_0X1500);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //1505
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x1500 jT809_0X1500 = new JT809_0x1500
                        {
                            VehicleColor = JT809VehicleColorType.蓝色,
                            VehicleNo = "桂DJB678",
                            SubBusinessType = JT809SubBusinessType.车辆应急接入监管平台应答消息.ToUInt16Value(),
                            SubBodies = JT809SubBusinessType.车辆应急接入监管平台应答消息.Create_车辆应急接入监管平台应答消息(
                                new JT809_0x1500_0x1505
                                {
                                    Result = JT809_0x1505_Result.无该车辆
                                })
                        };
                        var package = JT809BusinessType.主链路车辆监管消息.Create(header, jT809_0X1500);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                //9101
                Task.Run(() =>
                {
                    while (true)
                    {
                        JT809_0x9101 jT809_0X9101 = new JT809_0x9101
                        {
                            StartTime = 1584669924,
                            EndTime = 1584756324,
                            DynamicInfoTotal = uint.MaxValue
                        };
                        var package = JT809BusinessType.接收定位信息数量通知消息.Create(header, jT809_0X9101);
                        mainClient.SendAsync(new JT809Response(package, 256));
                        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                        Thread.Sleep(4000);
                    }
                });
                #region 从链路
                //9401
                //Task.Run(() =>
                //{
                //    while (true)
                //    {
                //        JT809_0x9400 jT809_0X9400 = new JT809_0x9400
                //        {
                //            VehicleNo = "桂DJB678",
                //            VehicleColor = JT809VehicleColorType.蓝色,
                //            SubBusinessType = JT809SubBusinessType.报警督办请求.ToUInt16Value(),
                //            SubBodies = JT809SubBusinessType.报警督办请求.Create_报警督办请求(
                //                new JT809_0x9400_0x9401
                //                {
                //                    WarnSrc = JT809WarnSrc.车载终端,
                //                    WarnType = JT809WarnType.疲劳驾驶报警.ToUInt16Value(),
                //                    WarnTime = DateTime.Now,
                //                    SupervisionID = "123FFAA1",
                //                    SupervisionEndTime = DateTime.Now,
                //                    SupervisionLevel = 3,
                //                    Supervisor = "算神",
                //                    SupervisorTel = "13907740944",
                //                    SupervisorEmail = "273279200@qq.com"
                //                })
                //        };
                //        var package = JT809BusinessType.从链路报警信息交互消息.Create(header, jT809_0X9400);
                //        mainClient.SendAsync(new JT809Response(package, 256));
                //        logger.LogDebug($"Thread:{Thread.CurrentThread.ManagedThreadId}-4s");
                //        Thread.Sleep(4000);
                //    }
                //});
                #endregion
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
