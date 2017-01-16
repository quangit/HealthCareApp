using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.File;
using HealthCare.Core.Models;
using HealthCare.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace HealthCare.Core.Services
{

   

	public class FileService : IFileService
	{
		private readonly IMvxFileStore _fileStore;
		private string AppName = "HealthCare";

        public Task<Dictionary<string, object>> LoadLocal()
        {
            var r = Load<Dictionary<string, object>>("_login.dat");
            return r;
        }
        public Task SaveLocal(bool remember, string userName, string password)
        {
            Data.UserName = userName;
            Data.Password = password;
            Data.Remember = remember;
            if (remember)
                Save("_login.dat",
                    new Dictionary<string, object>
                    {
                        {"username", userName},
                        {"password", password},
                        {"data", Data.User}
                    });
            else
            {
                Save("_login.dat",
                    new Dictionary<string, object>
                    {
                        {"username", ""},
                        {"password", ""},
                        {"data", Data.User}
                    });
            }
            return Task.FromResult((object)null);
        }

        public FileService(IMvxFileStore fileStore)
		{
			_fileStore = fileStore;
		}

		public Task Save(string filePath, object data)
		{
			var path = _fileStore.PathCombine(AppName, filePath);
			_fileStore.EnsureFolderExists(AppName);
            _fileStore.WriteFile(path, JsonConvert.SerializeObject(data));
            return Task.FromResult((object)null);

        }

        public Task<T> Load<T>(string filePath)
		{
			var path = _fileStore.PathCombine(AppName, filePath);
			var content = "";
			_fileStore.TryReadTextFile(path,out content);
			if (!string.IsNullOrEmpty(content))
			{
			    return Task.FromResult(JsonConvert.DeserializeObject<T>(content));
			}
            return Task.FromResult(default(T));

        }

        public Task Delete(string file)
	    {
            try
            {
				var path = _fileStore.PathCombine(AppName, file);
                _fileStore.DeleteFile(path);
            }
            catch (System.Exception)
            {
                //throw;
            }
            return Task.FromResult((object)null);
        }

        public Task SaveSetting()
        {
            Save("settings.dat", Data.Setting);
            return Task.FromResult((object)null);
        }
    }
}
