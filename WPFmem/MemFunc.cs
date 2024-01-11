using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFmem
{
    public class MemFunc
    {
        private List<Mem> memes = new List<Mem>();

        public void Load()
        {
            var json = File.ReadAllText("memes.json");
            memes = JsonConvert.DeserializeObject<List<Mem>>(json);
        }

        public void Save(TextBox TextName, TextBox TextURL, ComboBox cb_select, RadioButton radio_url, List<string> tags)
        {
            Mem meme = new Mem
            {
                Title = TextName.Text,
                Category = cb_select.Text,
                Tags = tags
            };
            if (radio_url.IsChecked == true)
            {
                meme.ImagePath = TextURL.Text;
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    meme.ImagePath = openFileDialog.FileName;
                }                    
            }
            memes.Add(meme);
            var json = JsonConvert.SerializeObject(memes);
            File.WriteAllText("memes.json", json);
        }

        public void Update(ListBox ListBoxMem)
        {
            ListBoxMem.Items.Clear();
            foreach (var meme in memes)
            {
                ListBoxMem.Items.Add(meme.Title);
            }
        }

        public void SearchTag(ListBox lb_mem, TextBox tbSearch)
        {
            string searchText = tbSearch.Text.ToLower();
            lb_mem.Items.Clear();
            foreach (var memas in memes)
            {
                if (memas.Tags != null && memas.Tags.Contains(searchText))
                {
                    lb_mem.Items.Add(memas.Title);
                }
            }
        }
        public void Delete(ListBox ListBoxMem)
        {
            var selectedMeme = memes.FirstOrDefault(meme => meme.Title == (string)ListBoxMem.SelectedItem);
            if (selectedMeme != null)
            {
                memes.Remove(selectedMeme);
                Update(ListBoxMem);
            }
            var json = JsonConvert.SerializeObject(memes);
            File.WriteAllText("memes.json", json);
        }

        public void ChangeSelectedMeme(ListBox ListBoxMem, Image ImageMem)
        {
            var selectedMeme = memes.FirstOrDefault(meme => meme.Title == (string)ListBoxMem.SelectedItem);
            if (selectedMeme != null)
            {
                ImageMem.Source = new BitmapImage(new Uri(selectedMeme.ImagePath));
            }
        }

        public void Filter(ListBox ListBoxMem, ComboBox ComboBoxCategory)
        {
            if (ComboBoxCategory.SelectedIndex == 0)
            {
                Update(ListBoxMem);
            }
            else
            {
                string category = (ComboBoxCategory.SelectedItem as ComboBoxItem).Content.ToString();
                var filteredMemes = memes.Where(meme => meme.Category == category).ToList();
                ListBoxMem.Items.Clear();
                foreach (var meme in filteredMemes)
                {
                    ListBoxMem.Items.Add(meme.Title);
                }
            }
        }
        public void Search(ListBox ListBoxMem, TextBox TextSearch)
        {
            string searchText = TextSearch.Text.ToLower();
            var filteredMemes = memes.Where(meme => meme.Title.ToLower().Contains(searchText)).ToList();
            ListBoxMem.Items.Clear();
            foreach (var meme in filteredMemes)
            {
                ListBoxMem.Items.Add(meme.Title);
            }
        }
    }
}
