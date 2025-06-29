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
using System.Windows.Shapes;
using OnTapCuoiKy_De1.Models;

namespace OnTapCuoiKy_De1
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        QlduocPhamContext db = new QlduocPhamContext();
        private void HienThiDataGrid()
        {
            var query1 = from thuoc in db.Thuocs
                         group thuoc by thuoc.MaDm into lthuocGroup
                         select new
                         {
                             Madm = lthuocGroup.Key,
                             TongTien = lthuocGroup.Sum(t =>  t.GiaBan * t.SoLuong)
                         };
            var query2 = from t in query1
                         join dmthuoc in db.DanhMucThuocs
                         on t.Madm equals dmthuoc.MaDm
                         select new
                         {
                             dmthuoc.MaDm,
                             dmthuoc.TenDm,
                             t.TongTien
                         };
            dgvThuoc.ItemsSource = query2.ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiDataGrid();
        }
    }
}
