using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Tiles;
using Microsoft.Band.Portable.Tiles.Pages;
using Microsoft.Band.Portable.Tiles.Pages.Data;
using System.Diagnostics;
#if !MVVMCROSS
using HealthCare.Win;
#endif
namespace HealthCare.Core.Services
{
    public interface IBandService
    {
        BandClient GetBandClient();
        Task<IEnumerable<BandDeviceInfo>> GetDevices();
        Task<bool> Connect(BandDeviceInfo bandInfo);
        Task SyncData();

    }
    public class BandService : IBandService
    {
        private BandClient _bandClient;
        short _titleId = 1;
        short _iconId = 2;
        short _dateId = 3;

        public BandClient GetBandClient()
        {
            return _bandClient;
        }

        public async Task<IEnumerable<BandDeviceInfo>> GetDevices()
        {
            var bandClientManager = BandClientManager.Instance;
            // query the service for paired devices
            return await bandClientManager.GetPairedBandsAsync();
        }

        public async Task<bool> Connect(BandDeviceInfo bandInfo)
        {

            var bandClientManager = BandClientManager.Instance;
            // connect to the first device
            if (bandInfo != null)
                _bandClient = await bandClientManager.ConnectAsync(bandInfo);
            return _bandClient != null;
        }

        private async Task<BandTile> CreateOrFindTile()
        {
            var tileManager = _bandClient.TileManager;
            // get the current set of tiles
            var tiles = await tileManager.GetTilesAsync();
            // get the number of tiles we can add
            var capacity = await tileManager.GetRemainingTileCapacityAsync();
            // create a new tile

            if (capacity == 0)
                return null;

            var ret = tiles.FirstOrDefault(t => t.Name.Equals(AppResources.ApplicationTitle));
            if (ret == null)
            {
                var tileId = Guid.NewGuid();
                ret = new BandTile(tileId)
                {
                    Icon = (await LoadImageResourceAsync("bandtile.png")),
                    Name = AppResources.ApplicationTitle,
                    SmallIcon = (await LoadImageResourceAsync("bandsmalltile.png")),
                };


                var pageLayout = new PageLayout
                {
                    Root = new ScrollFlowPanel
                    {
                        Orientation = FlowPanelOrientation.Vertical,
                        Rect = new PageRect(0, 0, 245, 105),
                        Elements =
                    {
                        new TextBlock(BandColor.FromHex("#FF00FF"))
                        {
                            ElementId = _titleId,
                            Rect = new PageRect(0,0,229,30),
                        },
                        new TextBlock()
                        {
                            ElementId = _dateId,
                            Rect = new PageRect(0,0,229,30),
                        },
                        new Icon
                        {
                            ElementId = _iconId,
                            Rect = new PageRect(0,0,229,46),
                        },
                    }
                    }
                };


                ret.PageLayouts.Add(pageLayout);
                // add additional images
                ret.PageImages.Add((await LoadImageResourceAsync("pageicon.png")));


                await _bandClient.TileManager.AddTileAsync(ret);
            }
            else
            {
                await _bandClient.TileManager.RemoveTilePagesAsync(ret.Id);
            }
            return ret;
        }
        public async Task SyncData()
        {
            if (_bandClient == null)
                return;
            var tileManager = _bandClient.TileManager;

            Guid pageId = Guid.NewGuid();
            int pageIndex = 0;
            var tile = await CreateOrFindTile();
            
            if ((Data.User.Schedules != null))
            {
                var schedules = Data.User.Schedules;
                //TODO: Nearest Time
                var s = from p in schedules
                        where p.StartDateTime >= DateTime.Now
                        select p;

                schedules = new System.Collections.ObjectModel.ObservableCollection<Schedule>(s.Reverse().ToList());
                var pageData = new List<PageData>();
                for (var i = 0; i < 10; i++)
                {
                    if (i >= schedules.Count)
                        break;


#pragma warning disable 4014
                    _bandClient.NotificationManager.SendMessageAsync(tile.Id, schedules[i].Hospital.Name, schedules[i].TimeExam, schedules[i].StartDateTime);
#pragma warning restore 4014


                    var pageDatum = new PageData
                    {
                        PageId = pageId,
                        PageLayoutIndex = pageIndex,
                        Data =
                        {
                            new TextBlockData
                            {
                                ElementId = _titleId,

                                Text = schedules[i].Hospital.Name
                            },
                            new TextBlockData
                            {
                                ElementId = _dateId,
                                Text = schedules[i].StartDateTime.ToString()
                            },
                            new ImageData
                            {
                                ElementId = _iconId,
                                ImageIndex = 0
                            }
                        }
                    };
                    pageId = Guid.NewGuid();
                    pageData.Add(pageDatum);
                }

                // apply the data to the tile
              //  await tileManager.SetTilePageDataAsync(tile.Id, pageData);
            }



            // add the tile

        }

        public static async Task<BandImage> LoadImageResourceAsync(string resourcePath)
        {
#if MVVMCROSS
            // get the resource stream from Embedded Resources
            var stream = await Task.Run(() =>
            {
                var assembly = typeof(HomeViewModel).GetTypeInfo().Assembly;
                resourcePath = typeof(App).Namespace + ".Assets." + resourcePath.Replace("/", ".").Replace("\\", ".");
                return assembly.GetManifestResourceStream(resourcePath);
            });
#else
            Windows.Storage.StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/" + resourcePath));
            var stream = await file.OpenStreamForReadAsync();
#endif

            // load the image
            return await BandImage.FromStreamAsync(stream);
        }
    }
}
