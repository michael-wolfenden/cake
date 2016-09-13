// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Cake.Common.Tools.OctopusDeploy;
using Cake.Core.IO;

namespace Cake.Common.Tests.Fixtures.Tools.OctopusDeploy
{
    internal sealed class OctopusDeployPusherFixture : OctopusDeployFixture<OctopusPushSettings>
    {
        internal string Server { get; set; }

        internal string ApiKey { get; set; }

        public List<FilePath> Packages { get; set; }

        public OctopusDeployPusherFixture()
        {
            Packages = new List<FilePath>
            {
                "MyPackage.1.0.0.zip",
                "MyOtherPackage.1.0.1.nupkg"
            };

            Server = "http://octopus";
            ApiKey = "API-12345";
        }

        protected override void RunTool()
        {
            var tool = new OctopusDeployPusher(FileSystem, Environment, ProcessRunner, Tools);
            tool.PushPackage(Server, ApiKey, Packages?.ToArray(), Settings);
        }
    }
}