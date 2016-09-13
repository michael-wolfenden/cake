// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Common.Tools.OctopusDeploy.Pack
{
    /// <summary>
    /// .NET Core project packer.
    /// </summary>
    public sealed class OctopusDeployPacker : OctopusDeployTool<OctopusDeployPackSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="OctopusDeployPacker" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public OctopusDeployPacker(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <summary>
        /// Package the project using the specified id, format and settings.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="format">The format.</param>
        /// <param name="settings">The settings.</param>
        public void Pack(string id, OctopusDeployPackFormat format, OctopusDeployPackSettings settings)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("No id specified.", nameof(id));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(id, format, settings));
        }

        private ProcessArgumentBuilder GetArguments(string id, OctopusDeployPackFormat format, OctopusDeployPackSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("pack");

            // Id
            builder.Append("--id");
            builder.Append(id);

            // Format
            builder.Append("--format");
            builder.Append(format.ToString());

            return builder;
        }
    }
}