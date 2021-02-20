using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TelephoneBook.Models.BasicModels;

namespace TelephoneBook.Repository
{
    public class TelephoneRepository
    {
        private static readonly string path = "Repository\\database.json";

        public async Task<List<TelephoneNote>> GetTelephoneNotes()
        {
            List<TelephoneNote> noteList;

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                noteList = await JsonSerializer.DeserializeAsync<List<TelephoneNote>>(fs);
                fs.Close();
            }

            return noteList;
        }

        public async Task AddTelephoneNote(TelephoneNote telephoneNote)
        {
            List<TelephoneNote> noteList;
            try
            {
                noteList = await GetTelephoneNotes();
                telephoneNote.Id = noteList.Max(x => x.Id) + 1;
            }
            catch
            {
                noteList = new List<TelephoneNote>();
                telephoneNote.Id = 1;
            }

            noteList.Add(telephoneNote);
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, noteList);
                fs.Close();
            }
        }

        public async Task RemoveTelephoneNote(TelephoneNote telephoneNote)
        {
            List<TelephoneNote> noteList = await GetTelephoneNotes();

            noteList.RemoveAll(x => x.Id == telephoneNote.Id);

            using (var fs = new FileStream(path, FileMode.Truncate))
            {
                await JsonSerializer.SerializeAsync(fs, noteList);
                fs.Close();
            }
        }

        public async Task UpdateTelephoneNote(TelephoneNote telephoneNote)
        {
            await RemoveTelephoneNote(telephoneNote);
            await AddTelephoneNote(telephoneNote);
        }
    }
}
