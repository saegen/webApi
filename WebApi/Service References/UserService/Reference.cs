﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.UserService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://dataservice/interfaces/user", ConfigurationName="UserService.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/CreateUser", ReplyAction="http://dataservice/interfaces/user/IUserService/CreateUserResponse")]
        Common.ApiUser CreateUser(Common.CreateUserDTO user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/CreateUser", ReplyAction="http://dataservice/interfaces/user/IUserService/CreateUserResponse")]
        System.Threading.Tasks.Task<Common.ApiUser> CreateUserAsync(Common.CreateUserDTO user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/DeleteUser", ReplyAction="http://dataservice/interfaces/user/IUserService/DeleteUserResponse")]
        void DeleteUser(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/DeleteUser", ReplyAction="http://dataservice/interfaces/user/IUserService/DeleteUserResponse")]
        System.Threading.Tasks.Task DeleteUserAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/GetUser", ReplyAction="http://dataservice/interfaces/user/IUserService/GetUserResponse")]
        Common.ApiUser GetUser(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/GetUser", ReplyAction="http://dataservice/interfaces/user/IUserService/GetUserResponse")]
        System.Threading.Tasks.Task<Common.ApiUser> GetUserAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/GetUsers", ReplyAction="http://dataservice/interfaces/user/IUserService/GetUsersResponse")]
        Common.ApiUser[] GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/GetUsers", ReplyAction="http://dataservice/interfaces/user/IUserService/GetUsersResponse")]
        System.Threading.Tasks.Task<Common.ApiUser[]> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/UpdateUser", ReplyAction="http://dataservice/interfaces/user/IUserService/UpdateUserResponse")]
        Common.ApiUser UpdateUser(Common.UpdateUserDTO user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dataservice/interfaces/user/IUserService/UpdateUser", ReplyAction="http://dataservice/interfaces/user/IUserService/UpdateUserResponse")]
        System.Threading.Tasks.Task<Common.ApiUser> UpdateUserAsync(Common.UpdateUserDTO user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : WebApi.UserService.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<WebApi.UserService.IUserService>, WebApi.UserService.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Common.ApiUser CreateUser(Common.CreateUserDTO user) {
            return base.Channel.CreateUser(user);
        }
        
        public System.Threading.Tasks.Task<Common.ApiUser> CreateUserAsync(Common.CreateUserDTO user) {
            return base.Channel.CreateUserAsync(user);
        }
        
        public void DeleteUser(int userId) {
            base.Channel.DeleteUser(userId);
        }
        
        public System.Threading.Tasks.Task DeleteUserAsync(int userId) {
            return base.Channel.DeleteUserAsync(userId);
        }
        
        public Common.ApiUser GetUser(int userId) {
            return base.Channel.GetUser(userId);
        }
        
        public System.Threading.Tasks.Task<Common.ApiUser> GetUserAsync(int userId) {
            return base.Channel.GetUserAsync(userId);
        }
        
        public Common.ApiUser[] GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public System.Threading.Tasks.Task<Common.ApiUser[]> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public Common.ApiUser UpdateUser(Common.UpdateUserDTO user) {
            return base.Channel.UpdateUser(user);
        }
        
        public System.Threading.Tasks.Task<Common.ApiUser> UpdateUserAsync(Common.UpdateUserDTO user) {
            return base.Channel.UpdateUserAsync(user);
        }
    }
}
