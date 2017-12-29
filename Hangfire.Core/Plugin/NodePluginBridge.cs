using Microsoft.AspNetCore.NodeServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hangfire.Plugin
{
    public class NodePluginBridge
    {
        private static NodePluginBridge _instance;

        private INodeServices _nodeService;

        private NodePluginBridge(INodeServices nodeServices)
        {
            _nodeService = nodeServices;
        }

        //
        // 摘要:
        //     Asynchronously invokes code in the Node.js instance.
        //
        // 参数:
        //   moduleName:
        //     The path to the Node.js module (i.e., JavaScript file) relative to your project
        //     root whose default CommonJS export is the function to be invoked.
        //
        //   args:
        //     Any sequence of JSON-serializable arguments to be passed to the Node.js function.
        //
        // 类型参数:
        //   T:
        //     The JSON-serializable data type that the Node.js code will asynchronously return.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task`1 representing the completion of the RPC call.
        public Task<T> InvokeAsync<T>(string moduleName, params object[] args)
        {
            return _nodeService.InvokeAsync<T>(moduleName, args);
        }
        //
        // 摘要:
        //     Asynchronously invokes code in the Node.js instance.
        //
        // 参数:
        //   cancellationToken:
        //     A System.Threading.CancellationToken that can be used to cancel the invocation.
        //
        //   moduleName:
        //     The path to the Node.js module (i.e., JavaScript file) relative to your project
        //     root whose default CommonJS export is the function to be invoked.
        //
        //   args:
        //     Any sequence of JSON-serializable arguments to be passed to the Node.js function.
        //
        // 类型参数:
        //   T:
        //     The JSON-serializable data type that the Node.js code will asynchronously return.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task`1 representing the completion of the RPC call.
        public Task<T> InvokeAsync<T>(CancellationToken cancellationToken, string moduleName, params object[] args)
        {
            return _nodeService.InvokeAsync<T>(cancellationToken, moduleName, args);
        }
        //
        // 摘要:
        //     Asynchronously invokes code in the Node.js instance.
        //
        // 参数:
        //   moduleName:
        //     The path to the Node.js module (i.e., JavaScript file) relative to your project
        //     root that contains the code to be invoked.
        //
        //   exportedFunctionName:
        //     Specifies the CommonJS export to be invoked.
        //
        //   args:
        //     Any sequence of JSON-serializable arguments to be passed to the Node.js function.
        //
        // 类型参数:
        //   T:
        //     The JSON-serializable data type that the Node.js code will asynchronously return.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task`1 representing the completion of the RPC call.
        public Task<T> InvokeExportAsync<T>(string moduleName, string exportedFunctionName, params object[] args)
        {
            return _nodeService.InvokeExportAsync<T>(moduleName, exportedFunctionName, args);
        }
        //
        // 摘要:
        //     Asynchronously invokes code in the Node.js instance.
        //
        // 参数:
        //   cancellationToken:
        //     A System.Threading.CancellationToken that can be used to cancel the invocation.
        //
        //   moduleName:
        //     The path to the Node.js module (i.e., JavaScript file) relative to your project
        //     root that contains the code to be invoked.
        //
        //   exportedFunctionName:
        //     Specifies the CommonJS export to be invoked.
        //
        //   args:
        //     Any sequence of JSON-serializable arguments to be passed to the Node.js function.
        //
        // 类型参数:
        //   T:
        //     The JSON-serializable data type that the Node.js code will asynchronously return.
        //
        // 返回结果:
        //     A System.Threading.Tasks.Task`1 representing the completion of the RPC call.
        public Task<T> InvokeExportAsync<T>(CancellationToken cancellationToken, string moduleName, string exportedFunctionName, params object[] args)
        {
            return _nodeService.InvokeExportAsync<T>(cancellationToken, moduleName, exportedFunctionName, args);
        }

        public static NodePluginBridge Instance
        {
            get
            {
                return _instance;
            }
        }

        internal static void Init(INodeServices nodeServices)
        {
            _instance = new NodePluginBridge(nodeServices);
        }
    }
}
