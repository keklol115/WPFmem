using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFmem
{
    public partial class MainWindow : Window
    {
        private MemFunc func;
        public MainWindow()
        {
            InitializeComponent();
            func = new MemFunc();
            func.Load();
            func.Update(lb_mem);
        }
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            func.Save(tb_Title, tb_URL, cb_category, radio_url, tb_Tag.Text.Split().ToList());
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            func.Search(lb_mem, tbSearch);
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            func.Delete(lb_mem);
        }
        private void ListBoxMem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            func.ChangeSelectedMeme(lb_mem, ImageMem);
        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            func.Filter(lb_mem, ComboBoxCategory);
        }

        private void Button_Tag_Click(object sender, RoutedEventArgs e)
        {
            func.SearchTag(lb_mem, tbSearch);
        }
    }
}
