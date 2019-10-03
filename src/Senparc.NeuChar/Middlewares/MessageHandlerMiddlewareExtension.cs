﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MessageHandlerMiddlewareExtension.cs
    文件功能描述：MessageHandler 中间件基类
    
    
    创建标识：Senparc - 20191003
    
----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.NeuChar.Middlewares
{
    /// <summary>
    /// MessageHandlerMiddleware 扩展类
    /// </summary>
    public static class MessageHandlerMiddlewareExtension
    {
        /// <summary>
        /// 使用 MessageHandler 配置。注意：会默认使用异步方法 messageHandler.ExecuteAsync()。
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="pathMatch">路径规则（路径开头，可带参数）</param>
        /// <param name="messageHandler">MessageHandler</param>
        /// <param name="options">设置选项</param>
        /// <returns></returns>
        public static IApplicationBuilder UseMessageHandler<TMC, TPM, TS>(this IApplicationBuilder builder, PathString pathMatch,
            Func<Stream, EncryptPostModel, int, MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>> messageHandler, Action<MessageHandlerMiddlewareOptions<TS>> options)
                where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
                where TPM : IEncryptPostModel
                where TS : class
        {
            return builder.Map(pathMatch, app =>
            {
                app.UseMiddleware<MessageHandlerMiddleware<TMC, TPM, TS>>(messageHandler, options);
            });
        }
    }
}

#endif