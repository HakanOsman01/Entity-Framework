namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context = new MusicHubDbContext();

            // DbInitializer.ResetDatabase(context);

        //    //string exportAlbum = ExportAlbumsInfo(context, 9);
            string exportSongs = ExportSongsAboveDuration(context, 4);
             Console.WriteLine(exportSongs);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();
            var albumInfo = context.Producers
                .Include(a=>a.Albums)
                .ThenInclude(a=>a.Songs)
                .ThenInclude(a=>a.Writer)
                .First(p => p.Id == producerId)
             .Albums.Select(a => new
             {
                 a.Name,
                 ReleaseDate = a.ReleaseDate,
                 ProducerName = a.Producer.Name,
                 AlbumSongs = a.Songs.Select(s => new
                 {
                     SongName=s.Name,
                     SongPrice=s.Price,
                     SongWriter = s.Writer.Name

                 }).OrderByDescending(s => s.SongName)
                 .ThenBy(s => s.SongWriter),
                 TotalAlbumPrice = a.Price

             }).OrderByDescending(a => a.TotalAlbumPrice)
             .AsEnumerable();
            foreach (var album in albumInfo)
            {
                int countSongs = 1;
                sb.AppendLine($"-AlbumName: {album.Name}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture)}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine("-Songs:");
                foreach (var song in album.AlbumSongs)
                {
                    sb.AppendLine($"---#{countSongs++}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.SongPrice:f2}");
                    sb.AppendLine($"---Writer: {song.SongWriter}");
                }
                sb.AppendLine($"-AlbumPrice: {album.TotalAlbumPrice:f2}");
            }

            return sb.ToString().Trim();
        }

       public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
       {
            StringBuilder sb = new StringBuilder();
            TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
            var songInfo = context.SongsPerformers
                .Include(p => p.Performer)
                .Include(s => s.Song)
                .Where(s => s.Song.Duration > timeSpan)
                .Select(s => new
                {
                    SongName = s.Song.Name,
                    WriterName = s.Song.Writer.Name,
                    PerformersInfo = s.Song.SongsPerformers.Select(sp=> new
                    {
                        FullName=sp.Performer.FirstName+' '+sp.Performer.LastName
                    }).OrderBy(sp=>sp.FullName).ToList(),
                    AlbumProducerName = s.Song.Album.Producer.Name,
                    DurationSong = s.Song.Duration

                })
                .OrderBy(s=>s.SongName)
                .ThenBy(w=>w.WriterName)
                .AsEnumerable();
         
            int countSongs = 1;

            foreach (var song in songInfo)
            {
                sb.AppendLine($"-Song #{countSongs++}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.WriterName}");

                    foreach (var performer in song.PerformersInfo)
                    {
                       
                       
                        
                        sb.AppendLine($"---Performer:{performer.FullName}");
                    }
               
               
               
                   
               
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducerName}");
                sb.AppendLine($"---Duration: {song.DurationSong.ToString("c")}");

            }
            return sb.ToString().Trim();




               
       }
    }
}

