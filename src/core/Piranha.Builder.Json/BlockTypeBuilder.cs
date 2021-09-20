using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Piranha.Builder.Json
{
    public class BlockTypeBuilder
    {
        #region Members
        private readonly List<ConfigFile> files = new List<ConfigFile>();
        private readonly IApi api;
        private readonly ILogger logger;
        #endregion

        public BlockTypeBuilder(IApi api, ILoggerFactory logFactory = null)
        {
            this.api = api;
            this.logger = logFactory?.CreateLogger("Piranha.Builder.Json.BlockTypeBuilder");
        }

        /// <summary>
        /// Adds a new json file to build block types from
        /// </summary>
        /// <param name="filename">The filename</param>
        /// <param name="optional">If the file is optional</param>
        /// <returns></returns>
        public BlockTypeBuilder AddJsonFile(string filename, bool optional = false)
        {
            files.Add(new ConfigFile()
            {
                Filename = filename,
                Optional = optional
            });
            return this;
        }

        /// <summary>
        /// Builds the block types.
        /// </summary>
        public void Build()
        {
            foreach (var file in files)
            {
                if (File.Exists(file.Filename))
                {
                    using (var json = File.OpenRead(file.Filename))
                    {
                        using (var reader = new StreamReader(json))
                        {
                            var import = JsonConvert.DeserializeObject<BlockTypeConfig>(reader.ReadToEnd());
                            import.AssertConfigIsValid();

                            foreach (var type in import.BlockTypes)
                            {
                                logger?.LogInformation($"Importing BlocType '{type.Id}'");
                                api.BlockTypes.Save(type);
                            }
                        }
                    }
                }
                else if (!file.Optional)
                {
                    logger?.LogError($"Specified file '{file.Filename}' not found.");
                    throw new FileNotFoundException($"Specified file {file.Filename} not found!");
                }
            }
        }
    }
}
