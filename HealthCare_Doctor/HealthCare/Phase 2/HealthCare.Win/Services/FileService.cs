using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using HealthCare.Core.Models;
using HealthCare.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace HealthCare.Win.Services
{
    public class FileService : IFileService
    {

        public async Task Save(string file, object data)
        {
            try
            {
                var fileIo = await ApplicationData.Current.LocalFolder.CreateFileAsync(file, CreationCollisionOption.OpenIfExists);
                using (var streamwriter = new StreamWriter(await fileIo.OpenStreamForWriteAsync()))
                {
                    await streamwriter.WriteAsync(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception)
            {
                //throw;
            }

        }

        public Task<T> Load<T>(string file)
        {
            return LoadFile<T>(file);
        }

        private async Task<T> LoadFile<T>(string file)
        {
            try
            {
                var fileIo = await ApplicationData.Current.LocalFolder.GetFileAsync(file);

                using (var streamReader = new StreamReader(await fileIo.OpenStreamForReadAsync()))
                {
                    return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public async Task Delete(string file)
        {
            try
            {
                var fileIo = await ApplicationData.Current.LocalFolder.GetFileAsync(file);
                if (fileIo != null)
                {
                    await fileIo.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

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

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Remember"))
            {
                ApplicationData.Current.LocalSettings.Values["Remember"] = remember;
            }
            else
            {                
                ApplicationData.Current.LocalSettings.Values.Add("Remember", remember);
            }

            if (remember)
                return Save("_login.dat",
                     new Dictionary<string, object>
                     {
                        {"username", userName},
                        {"password", password},
                        {"data", Data.User}
                     });
            else
            {
                return Save("_login.dat",
                     new Dictionary<string, object>
                     {
                        {"username", ""},
                        {"password", ""},
                        {"data", Data.User}
                     });
            }


        }

        public Task SaveSetting()
        {
            return Save("settings.dat", Data.Setting);
        }
    }
}
