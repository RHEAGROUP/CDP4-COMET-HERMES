﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationStateKind.cs" company="RHEA System S.A.">
// 
//    Copyright (c) 2023 RHEA System S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4.COMET.HERMES.Models.Enums
{
    /// <summary>
    /// An enumeration datatype that defines the possible kinds of authentication state
    /// </summary>
    public enum AuthenticationStateKind
    {
        /// <summary>
        /// Indicates that no authentication has been performed
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the given source address is impossible to fetch
        /// </summary>
        ServerFail,

        /// <summary>
        /// Indicates that authentication is in progress
        /// </summary>
        Authenticating,

        /// <summary>
        /// Indicates that the authentication was successful
        /// </summary>
        Success,

        /// <summary>
        /// Indicates that the authentication failed
        /// </summary>
        Fail
    }
}
