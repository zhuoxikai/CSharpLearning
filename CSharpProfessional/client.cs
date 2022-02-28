// using System;
// using System.ServiceModel;
// using System.ServiceModel.Description;
//
// namespace wcfRemoteProxy
// {
//     public class MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining
//         : System.ServiceModel.ClientBase<MicroServiceTraining_MicroServiceTraining.IWcfApi>,
//             MicroServiceTraining_MicroServiceTraining.IWcfApi
//     {
//         public MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining()
//         {
//         }
//
//         public MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining(
//             string endpointConfigurationName
//         ) : base(endpointConfigurationName)
//         {
//         }
//
//         public MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining(
//             string endpointConfigurationName,
//             string remoteAddress
//         ) : base(endpointConfigurationName, remoteAddress)
//         {
//         }
//
//         public MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining(
//             string endpointConfigurationName,
//             System.ServiceModel.EndpointAddress remoteAddress
//         ) : base(endpointConfigurationName, remoteAddress)
//         {
//         }
//
//         public MicroServiceTraining_MicroServiceTraining_IWcfApi_ClientV2_MicroServiceTraining(
//             System.ServiceModel.Channels.Binding binding,
//             System.ServiceModel.EndpointAddress remoteAddress
//         ) : base(binding, remoteAddress)
//         {
//             if (binding != null)
//             {
//                 if (binding is System.ServiceModel.WebHttpBinding)
//                     this
//                         .Endpoint
//                         .Behaviors
//                         .Add(new System.ServiceModel.Description.WebHttpBehavior());
//                 this
//                     .Endpoint
//                     .Behaviors
//                     .Add(new Beisen.ESB.ClientProxyFactory.Behaviors.EndpointBehaviors.ClientEndpointBehavior());
//             }
//
//             foreach (OperationDescription
//                     op
//                 in
//                 this.Endpoint.Contract.Operations
//             )
//             {
//                 DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior =
//                     op
//                             .Behaviors[typeof(
//                                 DataContractSerializerOperationBehavior
//                             )] as
//                         DataContractSerializerOperationBehavior;
//                 if (dataContractSerializerOperationBehavior != null)
//                 {
//                     dataContractSerializerOperationBehavior
//                         .MaxItemsInObjectGraph = int.MaxValue;
//                 }
//             }
//         }
//
//         public System.String GetTenantId(System.String msg)
//         {
//             return base.Channel.GetTenantId(msg);
//         }
//
//         public System.String GetIP(System.String msg)
//         {
//             return base.Channel.GetIP(msg);
//         }
//
//         public System.String HelloWorld(System.String msg)
//         {
//             return base.Channel.HelloWorld(msg);
//         }
//
//         public System.Boolean _ActivateService()
//         {
//             return base.Channel._ActivateService();
//         }
//
//         public System.Boolean _UnActivateService()
//         {
//             return base.Channel._UnActivateService();
//         }
//
//         public System.Collections.Generic.List<Beisen.MicroServiceTraining.ServiceInterface.Model.User>
//             GetUsers(System.Collections.Generic.List<System.Int32> userids)
//         {
//             return base.Channel.GetUsers(userids);
//         }
//
//         public Beisen.MicroServiceTraining.ServiceInterface.Model.User
//             GetUser(System.Int32 tenantid, System.Int32 userId)
//         {
//             return base.Channel.GetUser(tenantid, userId);
//         }
//
//         public System.Collections.Generic.List<Beisen.MicroServiceTraining.ServiceInterface.Model.User>
//             GetAllUser(System.Int32 count)
//         {
//             return base.Channel.GetAllUser(count);
//         }
//
//         public Beisen.MicroServiceTraining.ServiceInterface.Model.User
//             AddUser(System.Int32 tenantid)
//         {
//             return base.Channel.AddUser(tenantid);
//         }
//
//         public System.Boolean
//             DeleteUser(System.Int32 tenantid, System.Int32 userId)
//         {
//             return base.Channel.DeleteUser(tenantid, userId);
//         }
//
//         public System.Boolean
//             UpdateUser(System.Int32 tenantid, System.String name)
//         {
//             return base.Channel.UpdateUser(tenantid, name);
//         }
//
//         public Beisen.MicroServiceTraining.ServiceInterface.Model.ResultInfo<System.String>
//             Get(System.Int32 tenantid, System.Int32 userId)
//         {
//             return base.Channel.Get(tenantid, userId);
//         }
//     }
// }