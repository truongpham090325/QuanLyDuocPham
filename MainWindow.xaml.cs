using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OnTapCuoiKy_De1.Models;

namespace OnTapCuoiKy_De1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        QlduocPhamContext db = new QlduocPhamContext();
        private void HienThiDataGrid()
        {
            var query = from thuoc in db.Thuocs
                        where thuoc.SoLuong <= 200
                        orderby thuoc.TenThuoc
                        select new
                        {
                            thuoc.MaThuoc,
                            thuoc.TenThuoc,
                            thuoc.MaDm,
                            thuoc.GiaBan,
                            thuoc.SoLuong,
                            ThanhTien = thuoc.SoLuong * thuoc.GiaBan
                        };
            dgvThuoc.ItemsSource = query.ToList();
        }
        private void HienThiCBB()
        {
            var query = from dmthuoc in db.DanhMucThuocs
                        select dmthuoc;
            cboDMThuoc.ItemsSource = query.ToList();
            cboDMThuoc.DisplayMemberPath = "TenDm";
            cboDMThuoc.SelectedValuePath = "MaDm";
            cboDMThuoc.SelectedIndex = 0;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiDataGrid();
            HienThiCBB();
        }
        private bool KiemTraDL()
        {
            if(txtMaThuoc.Text == "" || txtTenThuoc.Text == "" || txtGiaBan.Text == "" || txtSoLuong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
                return false;
            }
            try
            {
                double giaBan = double.Parse(txtGiaBan.Text);
                if(giaBan <= 0)
                {
                    MessageBox.Show("Giá bán phải > 0", "Thông báo");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Giá bán phải là số thực", "Thông báo");
                return false;
            }
            if(!Regex.IsMatch(txtSoLuong.Text, @"\d+"))
            {
                MessageBox.Show("Số lượng phải là sô nguyên", "Thông báo");
                return false;
            }
            else
            {
                int soLuong = int.Parse(txtSoLuong.Text);
                if(soLuong <= 0)
                {
                    MessageBox.Show("Số lượng phải > 0", "Thông báo");
                    return false;
                }
            }
            return true;
        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (KiemTraDL())
                {
                    Thuoc t = new Thuoc();
                    t.MaThuoc = txtMaThuoc.Text;
                    t.TenThuoc = txtTenThuoc.Text;
                    t.MaDm = cboDMThuoc.SelectedValue.ToString();
                    t.GiaBan = double.Parse(txtGiaBan.Text);
                    t.SoLuong = int.Parse(txtSoLuong.Text);
                    db.Thuocs.Add(t);
                    db.SaveChanges();
                    HienThiDataGrid();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Có lỗi khi thêm: " + ex.Message, "Thông báo");
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            var thuocSua = db.Thuocs.SingleOrDefault(thuoc => thuoc.MaThuoc.Equals(txtMaThuoc.Text));
            if(thuocSua != null)
            {
                try
                {
                    if (KiemTraDL()) {
                        thuocSua.TenThuoc = txtTenThuoc.Text;
                        thuocSua.MaDm = cboDMThuoc.SelectedValue.ToString();
                        if(double.Parse(txtGiaBan.Text) <= 0 || int.Parse(txtSoLuong.Text) <= 0)
                        {
                            return;
                        }
                        thuocSua.GiaBan = double.Parse(txtGiaBan.Text);
                        thuocSua.SoLuong = int.Parse(txtSoLuong.Text);
                        db.SaveChanges();
                        HienThiDataGrid();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Có lỗi khi sửa: " + ex.Message, "Thông báo");
                }
            }
        }

        private void dgvThuoc_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(dgvThuoc.SelectedItem != null)
            {
                Type t = dgvThuoc.SelectedItem.GetType();
                PropertyInfo[] p = t.GetProperties();
                txtMaThuoc.Text = p[0].GetValue(dgvThuoc.SelectedValue).ToString();
                txtTenThuoc.Text = p[1].GetValue(dgvThuoc.SelectedValue).ToString();
                cboDMThuoc.SelectedValue = p[2].GetValue(dgvThuoc.SelectedValue).ToString();
                txtGiaBan.Text = p[3].GetValue(dgvThuoc.SelectedValue).ToString();
                txtSoLuong.Text = p[4].GetValue(dgvThuoc.SelectedValue).ToString();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            var thuocXoa = db.Thuocs.SingleOrDefault(thuoc=>thuoc.MaThuoc.Equals(txtMaThuoc.Text));
            if(thuocXoa != null)
            {
                MessageBoxResult mbr = MessageBox.Show("Bạn có chắc muốn xóa không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(mbr == MessageBoxResult.Yes)
                {
                    db.Thuocs.Remove(thuocXoa);
                    db.SaveChanges();
                    HienThiDataGrid();
                }
            }
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            Window2 wd2 = new Window2();
            wd2.Show();
        }
    }
}