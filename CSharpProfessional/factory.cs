// using System;
// using System.ServiceModel;
// using System.ServiceModel.Description;
// using Beisen.ESB.ClientProxyFactory;
// using Beisen.ESB.ClientProxyFactory.Generator;
// using Beisen.ESB.ClientProxyFactory.LoadBalance;
// using Beisen.ESB.ClientProxyFactory.Utility;
// using Beisen.ESB.ClientProxyFactory.Proxy;
// using Beisen.ESB.ClientProxyFactory.Node;
// using Beisen.ESB.ClientProxyFactory.Configs;
// using Beisen.ESB.ClientProxyFactory.Configs.Bind;
// using Beisen.ESB.ClientProxyFactory.Subscription;
// using Beisen.Common;
// using Beisen.Common.Context;
// using Beisen.Configuration;
// using Beisen.Configuration.Logging;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Reflection;
// using System.Linq;
// using System.Text;
// using System.Net.Http;
// using Newtonsoft.Json;
// using Beisen.Log.Trace;
// using Beisen.Log.Trace.Entity;
// using Beisen.Common.ESB.Verb;
// using System.Runtime.Remoting.Messaging;
//
// namespace wcfRemoteProxy
// {
//     public class MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientFactoryV2_MicroServiceTraining :
//         IWcfClientProxyFactory<MicroServiceTraining_MicroServiceTraining.IWcfApi>,
//         MicroServiceTraining_MicroServiceTraining.IWcfApi
//     {
//         #region var
//
//         private string _serviceId;
//         private static readonly Beisen.Logging.LogWrapper _logger = new Beisen.Logging.LogWrapper();
//
//         private WcfLoadBalanceStrategy loadBalanceStrategy =
//             new WcfLoadBalanceStrategy("MicroServiceTraining_MicroServiceTraining");
//
//         private IDictionary<string, WcfProxyConnectPool<MicroServiceTraining_MicroServiceTraining.IWcfApi>> _poolList =
//             new Dictionary<string, WcfProxyConnectPool<MicroServiceTraining_MicroServiceTraining.IWcfApi>>();
//
//         private static object _lockSyncObject = new object();
//
//         #endregion
//         
//
//
//         public MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientFactoryV2_MicroServiceTraining(string serviceId)
//         {
//             _serviceId = serviceId;
//             CreateChannelPool();
//         }
//
//         public ClientBase<MicroServiceTraining_MicroServiceTraining.IWcfApi> CreateProxy(string address)
//         {
//             return new MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining(
//                 WcfNetTcpBinding.Instance.Binding, new EndpointAddress(address));
//         }
//
//         public void CreateChannelPool()
//         {
//             _logger.Info("Create channel pool :MicroServiceTraining_MicroServiceTraining.IWcfApi");
//             foreach (ComponentNodeStatus node in SubscriptionNodeManager.Instance.GetAvailableNodes(_serviceId))
//             {
//                 string address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 if (!_poolList.ContainsKey(address))
//                 {
//                     _poolList.Add(address,
//                         new WcfProxyConnectPool<MicroServiceTraining_MicroServiceTraining.IWcfApi>(this,
//                             ESBClientConfig.Instance.ChannelPool));
//                     _logger.Info("pool size:" + ESBClientConfig.Instance.ChannelPool.PoolSize + " address:" + address);
//                 }
//             }
//
//             _logger.Info(
//                 "Initialize MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientFactoryV2_MicroServiceTraining, serviceName:MicroServiceTraining_MicroServiceTraining.IWcfApi");
//         }
//
//         private WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> GetProxyFromPool(
//             string address, bool usePool)
//         {
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             if (_poolList.Count > 0 && usePool)
//             {
//                 if (_poolList.ContainsKey(address) && _poolList[address] != null)
//                 {
//                     proxy = _poolList[address].GetProxy(address);
//                     if (proxy == null)
//                     {
//                         proxy = new WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi>()
//                         {
//                             CommunicationObject = CreateProxy(address),
//                             ExpireTime = DateTime.Now.AddMinutes(ESBClientConfig.Instance.ChannelPool.WaitingTimeout)
//                         };
//                         ;
//                         _logger.Info(address + " proxy new");
//                     }
//                     else
//                     {
//                         _logger.Info(address + " proxy from pool");
//                     }
//                 }
//                 else
//                 {
//                     proxy = new WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi>()
//                     {
//                         CommunicationObject = CreateProxy(address),
//                         ExpireTime = DateTime.Now.AddMinutes(ESBClientConfig.Instance.ChannelPool.WaitingTimeout)
//                     };
//                     _logger.Info(address + " proxy new.");
//                 }
//             }
//             else
//             {
//                 proxy = new WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi>()
//                 {
//                     CommunicationObject = CreateProxy(address),
//                     ExpireTime = DateTime.Now.AddMinutes(ESBClientConfig.Instance.ChannelPool.WaitingTimeout)
//                 };
//                 _logger.Info(address + " proxy new..");
//             }
//
//             return proxy;
//         }
//
//         private void ReturnProxyToPool(string address,
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy)
//         {
//             if (proxy != null && address != "")
//             {
//                 if (_poolList.ContainsKey(address))
//                 {
//                     _poolList[address].CheckInPool(proxy);
//                     _logger.Info("return pool , size:" + ESBClientConfig.Instance.ChannelPool.PoolSize + " address:" +
//                                  address);
//                 }
//                 else
//                 {
//                     lock (_lockSyncObject)
//                     {
//                         if (!_poolList.ContainsKey(address))
//                         {
//                             _poolList.Add(address,
//                                 new WcfProxyConnectPool<MicroServiceTraining_MicroServiceTraining.IWcfApi>(this,
//                                     ESBClientConfig.Instance.ChannelPool));
//                             _logger.Info("create new pool , size:" + ESBClientConfig.Instance.ChannelPool.PoolSize +
//                                          " address:" + address);
//                         }
//                     }
//
//                     _poolList[address].CheckInPool(proxy);
//                 }
//             }
//         }
//
//
//         private void RefreshNodeStatus(Type errorType, ComponentNodeStatus node, NodeAction action)
//         {
//             if (node == null)
//                 return;
//
//             if (errorType == null && NodeAction.FailedZero == action)
//             {
//                 SubscriptionNodeManager.Instance.RefreshComponentNodeFailedNumber(_serviceId, node.IP, node.TcpPort,
//                     action);
//                 _logger.Info("FailedZeroNodeStatus" + node.IP + ":" + node.TcpPort);
//                 return;
//             }
//
//             if (errorType == typeof(CommunicationException) || errorType == typeof(CommunicationObjectFaultedException))
//             {
//                 switch (action)
//                 {
//                     case NodeAction.Enable:
//                         SubscriptionNodeManager.Instance.RefreshComponentNodeStatus(_serviceId, node.IP, node.TcpPort,
//                             MessageType.NodeEnable);
//                         _logger.Info("EnableNodeStatus" + node.IP + ":" + node.TcpPort + "," + errorType.ToString());
//                         break;
//
//                     case NodeAction.Disable:
//                         SubscriptionNodeManager.Instance.RefreshComponentNodeStatus(_serviceId, node.IP, node.TcpPort,
//                             MessageType.NodeDisable);
//                         _logger.Info("DisableNodeStatus" + node.IP + ":" + node.TcpPort + "," + errorType.ToString());
//                         break;
//
//                     case NodeAction.FailedIncrease:
//                         SubscriptionNodeManager.Instance.RefreshComponentNodeFailedNumber(_serviceId, node.IP,
//                             node.TcpPort, action);
//                         _logger.Info("FailedIncreaseNodeStatus" + node.IP + ":" + node.TcpPort + "," +
//                                      errorType.ToString());
//                         break;
//
//                     case NodeAction.FailedDecrease:
//                         SubscriptionNodeManager.Instance.RefreshComponentNodeFailedNumber(_serviceId, node.IP,
//                             node.TcpPort, action);
//                         _logger.Info("FailedDecreaseNodeStatus" + node.IP + ":" + node.TcpPort + "," +
//                                      errorType.ToString());
//                         break;
//                 }
//             }
//         }
//
//         private void WriteToKafka(string methodFullName, string[] parameterTypes, string[] parameterNames,
//             string[] parameterValues, string errorType)
//         {
//             if (KafkaQueueManager.Instance.Count >= ESBClientReadWriteConfig.Instance.QueueSize)
//             {
//                 _logger.Debug("message discard [queue full],current queue size:{" + KafkaQueueManager.Instance.Count +
//                               "}");
//                 return;
//             }
//
//             var data = new ContextData();
//             data.ErrorType = errorType;
//             data.DateTime = DateTime.Now;
//             data.MethodFullName = methodFullName;
//             data.ParameterTypes = parameterTypes;
//             data.ParameterNameStrings = parameterNames;
//             data.ParameterValueStrings = parameterValues;
//             data.TenantId = Beisen.Common.Context.ApplicationContext.Current.TenantId;
//             data.ApplicationContext = Beisen.Common.Context.ApplicationContext.Current.ToJson();
//             data.CloudAppName = Beisen.Common.Context.ApplicationContext.Current.ApplicationName;
//             data.ClientAppName = System.Configuration.ConfigurationManager.AppSettings["applicationName"];
//             KafkaQueueManager.Instance.Enqueue(JsonConvert.SerializeObject(data));
//         }
//
//
//         private void CloseProxy(WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy)
//         {
//             if (proxy != null)
//             {
//                 proxy.CommunicationObject.Abort();
//                 proxy = null;
//             }
//         }
//
//
//         private void CloseChannelPool()
//         {
//             foreach (ComponentNodeStatus node in SubscriptionNodeManager.Instance.GetAvailableNodes(_serviceId))
//             {
//                 string address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 if (_poolList.ContainsKey(address))
//                     _poolList[address].ClosePool();
//             }
//
//             _poolList.Clear();
//         }
//
//         public void CloseChannelPool(string ip, int port)
//         {
//             string address = string.Format("net.tcp://{0}:{1}", ip, port);
//             if (_poolList.ContainsKey(address))
//                 _poolList[address].ClosePool();
//         }
//
//
//         public System.String GetTenantId(System.String msg)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining).GetTenantId(msg);
//             }
//
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetTenantId failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetTenantId , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .GetTenantId(msg);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetTenantId , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetTenantId succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.String GetIP(System.String msg)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining).GetIP(msg);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetIP failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetIP , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .GetIP(msg);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetIP , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetIP succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.String HelloWorld(System.String msg)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining).HelloWorld(msg);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.HelloWorld failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.HelloWorld , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .HelloWorld(msg);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.HelloWorld , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.HelloWorld succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.Boolean _ActivateService()
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)._ActivateService();
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._ActivateService failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._ActivateService , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 ._ActivateService();
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._ActivateService , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._ActivateService succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.Boolean _UnActivateService()
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                         MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                     ._UnActivateService();
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._UnActivateService failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._UnActivateService , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 ._UnActivateService();
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._UnActivateService , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider._UnActivateService succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.Collections.Generic.List<Beisen.MicroServiceTraining.ServiceInterface.Model.User> GetUsers(
//             System.Collections.Generic.List<System.Int32> userids)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining).GetUsers(userids);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUsers failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUsers , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .GetUsers(userids);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUsers , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUsers succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public Beisen.MicroServiceTraining.ServiceInterface.Model.User GetUser(System.Int32 tenantid,
//             System.Int32 userId)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                         MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                     .GetUser(tenantid, userId);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUser failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUser , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .GetUser(tenantid, userId);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUser , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetUser succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.Collections.Generic.List<Beisen.MicroServiceTraining.ServiceInterface.Model.User> GetAllUser(
//             System.Int32 count)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining).GetAllUser(count);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetAllUser failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetAllUser , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .GetAllUser(count);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetAllUser , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.GetAllUser succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public Beisen.MicroServiceTraining.ServiceInterface.Model.User AddUser(System.Int32 tenantid)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining).AddUser(tenantid);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.AddUser failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.AddUser , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .AddUser(tenantid);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.AddUser , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.AddUser succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.Boolean DeleteUser(System.Int32 tenantid, System.Int32 userId)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                         MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                     .DeleteUser(tenantid, userId);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.DeleteUser failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.DeleteUser , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .DeleteUser(tenantid, userId);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.DeleteUser , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.DeleteUser succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public System.Boolean UpdateUser(System.Int32 tenantid, System.String name)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                         MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                     .UpdateUser(tenantid, name);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.UpdateUser failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.UpdateUser , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .UpdateUser(tenantid, name);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.UpdateUser , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.UpdateUser succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//         public Beisen.MicroServiceTraining.ServiceInterface.Model.ResultInfo<System.String> Get(System.Int32 tenantid,
//             System.Int32 userId)
//
//         {
//             bool isSucceed = true;
//             string address = "";
//             ComponentNodeStatus node = null;
//             var behaviorMode = Behavior.None;
//             WcfCommunicationObject<MicroServiceTraining_MicroServiceTraining.IWcfApi> proxy = null;
//             try
//             {
//                 node = loadBalanceStrategy.GetAvailableNode();
//                 address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                 proxy = GetProxyFromPool(address, true);
//                 CallContext.LogicalSetData("Trace_Invoking_Address", address);
//
//                 return (proxy.CommunicationObject as
//                         MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                     .Get(tenantid, userId);
//             }
//             catch (Exception ex)
//             {
//
//                 if (ex.GetType() == typeof(TimeoutException))
//                 {
//                     var traceLogEntity = CallContext.LogicalGetData("Trace_LogEntity") as TraceLogEntity;
//                     traceLogEntity.ResultType = ResultType.Error;
//                     traceLogEntity.CustomeInfo = "Please view server-side log,write timeout,address:" + address;
//                     traceLogEntity.CostInMillisecond =
//                         (long) (DateTime.Now - traceLogEntity.CurrentDateTime).TotalMilliseconds;
//                     TraceLogClient.Instance.Save(traceLogEntity);
//                     _logger.Error(ex);
//
//                 }
//                 else if (ex.GetType() == typeof(CommunicationException) ||
//                          ex.GetType() == typeof(EndpointNotFoundException) ||
//                          ex.GetType() == typeof(CommunicationObjectFaultedException) ||
//                          ex.GetType() == typeof(CommunicationObjectAbortedException))
//                 {
//                     isSucceed = false;
//                     RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                     CloseProxy(proxy);
//
//                     _logger.Debug(
//                         "Message:Call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.Get failed,address:" +
//                         address + ",will try retry:" + ESBClientConfig.Instance.TryCall.Number + ",error message:" +
//                         (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
//                     for (int i = 0; i < ESBClientConfig.Instance.TryCall.Number; i++)
//                     {
//                         bool isTrySucceed = true;
//                         try
//                         {
//                             node = loadBalanceStrategy.GetAvailableNode();
//                             address = string.Format("net.tcp://{0}:{1}", node.IP, node.TcpPort);
//                             proxy = GetProxyFromPool(address, false);
//                             CallContext.LogicalSetData("Trace_Invoking_Address", address);
//                             _logger.Debug(String.Format(
//                                 @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.Get , calling, {0} th,address:{1}",
//                                 i + 1, address));
//                             return (proxy.CommunicationObject as
//                                     MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining)
//                                 .Get(tenantid, userId);
//                         }
//
//                         catch (Exception rex)
//                         {
//                             _logger.Debug(String.Format(
//                                 @"Message:Failed try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.Get , calling, {0} th,address:{1},error message:{2}",
//                                 i + 1, address,
//                                 (rex.InnerException != null ? rex.InnerException.Message : rex.Message)));
//                             isTrySucceed = false;
//                             isSucceed = false;
//                             RefreshNodeStatus(ex.GetType(), node, NodeAction.FailedIncrease);
//                             CloseProxy(proxy);
//                             if (i == ESBClientConfig.Instance.TryCall.Number - 1)
//                             {
//                                 _logger.Error(rex);
//
//                             }
//                         }
//                         finally
//                         {
//                             if (isTrySucceed)
//                             {
//                                 isSucceed = true;
//                                 _logger.Debug(String.Format(
//                                     @"Message:Try reconnect call Beisen.MicroServiceTraining.ServiceInterface.IUserProvider.Get succeed ,address:{0}",
//                                     address));
//                             }
//                         }
//                     }
//
//                     _logger.Error(ex);
//                 }
//                 else
//                 {
//                     _logger.Error(ex);
//                 }
//
//                 throw ex;
//             }
//
//             finally
//             {
//                 if (isSucceed && node != null)
//                     RefreshNodeStatus(null, node, NodeAction.FailedZero);
//                 ReturnProxyToPool(address, proxy);
//             }
//
//         }
//
//     }
//     
// }