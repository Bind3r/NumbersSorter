namespace NumbersSorter.Services
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using NumbersSorter.Interfaces;
    using NumbersSorter.Models;

    public class JsonFileHandlingService : IFileHandlingService
    {
        private readonly ILogger<JsonFileHandlingService> _logger;
        private readonly FileConfig _fileConfig;

        public JsonFileHandlingService(ILogger<JsonFileHandlingService> logger, IOptions<FileConfig> fileConfig)
        {
            _logger = logger;
            _fileConfig = fileConfig.Value;
        }

        public bool TryReadFile(out string error, out string result)
        {
            result = null;
            error = null;

            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine(_fileConfig.Path, _fileConfig.Name)))
                {
                    result = reader.ReadToEnd();
                }

                return true;
            }
            catch (ArgumentNullException e)
            {
                error = "Missing Path Parts";
                _logger.LogError(error);
            }
            catch (FileNotFoundException e)
            {
                error = "No File Found";
                _logger.LogWarning(error);
            }
            catch (Exception e)
            {
                error = "Failed to read file";
                _logger.LogError(error, e);
            }

            return false;
        }

        public bool TryWriteToFile(object source, out string error)
        {
            error = null;

            try
            {
                string jsonStringSource = JsonConvert.SerializeObject(source);
                using (StreamWriter writer = new StreamWriter(Path.Combine(_fileConfig.Path, _fileConfig.Name)))
                {
                    writer.Write(jsonStringSource);
                }

                return true;
            }
            catch (ArgumentNullException e)
            {
                error = "Missing Path";
                _logger.LogError(error, e);
            }
            catch (Exception e)
            {
                error = "Failed to write to file";
                _logger.LogError(error, e);
            }

            return false;
        }
    }
}