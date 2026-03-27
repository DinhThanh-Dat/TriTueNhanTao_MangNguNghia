using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Diagnostics;

namespace tamgiac_mangngunghia
{
    public partial class Form1 : Form
    {
        #region Điều Khiển
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cbbgiatritinh.SelectedItem = cbbgiatritinh.Items[0];
            Arr();
            HienThiDGV();
        }
        #endregion

        #region Khai Báo Biến

        public string[] YEUTO = { "α", "β", "δ", "a", "b", "c", "p", "S", "h" };
        private float congthuc = 6, yeuto = 9;
        private float[,] a = new float[9, 6];
        private const float goc = 180f;
        private float kq = 0;
        private float[,] ArrLuu = new float[9, 6];
        private int stop;
        bool flag;

        // ********* THÊM CÁC BIẾN LƯU GIÁ TRỊ CẠNH VÀ GÓC CUỐI CÙNG *********
        private float final_a = 0, final_b = 0, final_c = 0;
        private float final_alpha = 0, final_beta = 0, final_delta = 0;

        // Khai báo trong Form Class (trên cùng, cùng chỗ với các biến global khác)
        private Stopwatch stopwatch;
        #endregion

        #region Khởi Tạo Mảng
        private void Arr()
        {
            float temp = -1;
            for (int i = 0; i < yeuto; i++)
                for (int j = 0; j < congthuc; j++)
                {
                    a[i, j] = 0;
                    ArrLuu[i, j] = 0;
                }
            //Mảng xử lý
            a[0, 0] = a[0, 3] = temp;
            a[1, 0] = a[1, 1] = a[1, 3] = temp;
            a[2, 1] = a[2, 3] = temp;
            a[3, 0] = a[3, 2] = a[3, 5] = temp;
            a[4, 0] = a[4, 1] = a[4, 2] = a[4, 5] = temp;
            a[5, 1] = a[5, 2] = a[5, 4] = a[5, 5] = temp;
            a[6, 2] = a[6, 5] = temp;
            a[7, 2] = a[7, 4] = temp;
            a[8, 4] = temp;
            //Mảng xuất ra DataGridView
            ArrLuu[0, 0] = ArrLuu[0, 3] = temp;
            ArrLuu[1, 0] = ArrLuu[1, 1] = ArrLuu[1, 3] = temp;
            ArrLuu[2, 1] = ArrLuu[2, 3] = temp;
            ArrLuu[3, 0] = ArrLuu[3, 2] = ArrLuu[3, 5] = temp;
            ArrLuu[4, 0] = ArrLuu[4, 1] = ArrLuu[4, 2] = ArrLuu[4, 5] = temp;
            ArrLuu[5, 1] = ArrLuu[5, 2] = ArrLuu[5, 4] = ArrLuu[5, 5] = temp;
            ArrLuu[6, 2] = ArrLuu[6, 5] = temp;
            ArrLuu[7, 2] = ArrLuu[7, 4] = temp;
            ArrLuu[8, 4] = temp;
        }
        #endregion

        #region Xử Lý

        //Kiểm tra xem giá trị ở combobox đã được tính chưa.
        private bool Tinhgiatri()
        {
            switch (cbbgiatritinh.SelectedIndex)
            {
                case 1:
                    if (a[0, 0] == -1)
                    {
                        break;
                    }
                    return true;

                case 2:
                    if (a[1, 0] == -1)
                    {
                        break;
                    }
                    return true;
                case 3:
                    if (a[2, 1] == -1)
                    {
                        break;
                    }
                    return true;
                case 4:
                    if (a[3, 0] == -1)
                    {
                        break;
                    }
                    return true;
                case 5:
                    if (a[4, 0] == -1)
                    {
                        break;
                    }
                    return true;
                case 6:
                    if (a[5, 1] == -1)
                    {
                        break;
                    }
                    return true;
                case 7:
                    if (a[7, 2] == -1)
                    {
                        break;
                    }
                    return true;
                case 8:
                    if (a[8, 4] == -1)
                    {
                        break;
                    }
                    return true;
                case 9:
                    if (a[6, 2] == -1)
                    {
                        break;
                    }
                    return true;
            }
            return false;
        }

        //Hiển thị lên Textbox kết quả.
        double RagToDeg(double rad)
        {
            return rad * 180 / Math.PI;
        }
        public void HienThiKetQua()
        {
            float val_alpha = 0, val_beta = 0, val_delta = 0;
            float val_a = 0, val_b = 0, val_c = 0;

            // Lấy các giá trị cuối cùng từ ma trận 'a' (chuyển góc từ Radian sang Độ)
            if (a[0, 0] > 0) val_alpha = (float)(a[0, 0] * goc / Math.PI);
            if (a[1, 0] > 0) val_beta = (float)(a[1, 0] * goc / Math.PI);
            if (a[2, 1] > 0) val_delta = (float)(a[2, 1] * goc / Math.PI);

            if (a[3, 0] > 0) val_a = a[3, 0];
            if (a[4, 0] > 0) val_b = a[4, 0];
            if (a[5, 1] > 0) val_c = a[5, 1];

            // Cập nhật các biến global để vẽ
            final_alpha = val_alpha;
            final_beta = val_beta;
            final_delta = val_delta;
            final_a = val_a;
            final_b = val_b;
            final_c = val_c;

            // Hiển thị kết quả vào TextBox kết quả chính (txtketqua)
            switch (cbbgiatritinh.SelectedIndex)
            {
                case 1:
                    txtketqua.Text = final_alpha.ToString("0.##");
                    break;
                case 2:
                    txtketqua.Text = final_beta.ToString("0.##");
                    break;
                case 3:
                    txtketqua.Text = final_delta.ToString("0.##");
                    break;
                case 4:
                    txtketqua.Text = final_a.ToString("0.##");
                    break;
                case 5:
                    txtketqua.Text = final_b.ToString("0.##");
                    break;
                case 6:
                    txtketqua.Text = final_c.ToString("0.##");
                    break;
                case 7:
                    txtketqua.Text = a[7, 2].ToString("0.##"); // S
                    break;
                case 8:
                    txtketqua.Text = a[8, 4].ToString("0.##"); // h
                    break;
                case 9:
                    txtketqua.Text = a[6, 2].ToString("0.##"); // p
                    break;
            }
        }


        // Phương thức vẽ tam giác
        private void VeTamGiac(Graphics g, int width, int height)
        {           
            if (final_a <= 0 || final_b <= 0 || final_c <= 0)
            {
                g.DrawString("Lỗi: Chưa đủ 3 cạnh (a, b, c) để vẽ hình tam giác.", this.Font, Brushes.Gray, 10, 10);
                return;
            }
            if (final_a + final_b <= final_c || final_a + final_c <= final_b || final_b + final_c <= final_a)
            {
                g.DrawString("3 cạnh đã tính không thỏa mãn điều kiện tam giác.", this.Font, Brushes.Red, 10, 10);
                return;
            }

            // --- 2. TÍNH TOÁN VÀ THIẾT LẬP TỶ LỆ ---
            double cosAlpha = (Math.Pow(final_b, 2) + Math.Pow(final_c, 2) - Math.Pow(final_a, 2)) / (2 * final_b * final_c);
            cosAlpha = Math.Max(-1.0, Math.Min(1.0, cosAlpha));
            double rad_alpha = Math.Acos(cosAlpha);

            float maxSide = Math.Max(Math.Max(final_a, final_b), final_c);
            float padding = 40; 
            float scale = Math.Min((width - 2 * padding) / maxSide, (height - 2 * padding) / maxSide);

            // Đặt đỉnh A (Góc dưới bên trái)
            PointF A = new PointF(padding, height - padding);

            // Đỉnh B: Cách A một đoạn c (cạnh c là AB) nằm ngang
            PointF B = new PointF(A.X + final_c * scale, A.Y);

            // Tính tọa độ đỉnh C
            float Cx = (float)(A.X + final_b * scale * Math.Cos(rad_alpha));
            float Cy = (float)(A.Y - final_b * scale * Math.Sin(rad_alpha));
            PointF C = new PointF(Cx, Cy);

            PointF[] vertices = { A, B, C };

            // --- 3. VẼ TAM GIÁC ---
            g.Clear(Color.FromKnownColor(KnownColor.Control));
            Pen pen = new Pen(Color.Blue, 2);
            g.DrawPolygon(pen, vertices);

            // --- 4. CẤU HÌNH FONT VÀ BRUSH CƠ BẢN ---
            Font fontVertex = new Font("Arial", 10, FontStyle.Bold);
            Brush brushVertex = Brushes.Black;
            Pen penDefault = new Pen(Color.Black, 1);

            // 5. GỌI HÀM HIỂN THỊ CÁC THÔNG SỐ (đã tách ra)
            HienThiThongSoTrenHinh(g, A, B, C, fontVertex, brushVertex, penDefault);

            // --- 6. HIỂN THỊ LOẠI TAM GIÁC ---
            string loaiTamGiac = XacDinhLoaiTamGiac();
            Font fontLoai = new Font("Arial", 12, FontStyle.Bold);

            // Hiển thị ở vị trí cố định 10, 10
            g.DrawString(
                "Loại: " + loaiTamGiac,
                fontLoai,
                Brushes.DarkGreen,
                10,  
                10   
            );
        }

        //Phương thức này sẽ kiểm tra xem yếu tố nào có thể tính.
        private int LayYeuTo(int congthuc1)
        {
            int dem = 0, gt = -1;
            for (int i = 0; i < yeuto; i++)
                if (a[i, congthuc1] == -1)
                {
                    dem++;
                    gt = i;
                }
            if (dem == 1)
                return gt;
            return -1;
        }

        //Kích hoạt cơ chế lan truyền.
        private void Cochelantruyen(int congthuc1, int yeuto1)
        {
            float value = -1, lgt = -1;
            switch (congthuc1)
            {
                case 0:
                    switch (yeuto1)
                    {
                        case 0:
                            // α = asin(a * sinβ / b)  // Tính radian
                            if (a[4, 0] == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (cạnh b = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            lgt = (float)((a[3, 0] * Math.Sin(a[1, 0])) / (a[4, 0]));
                            if (Math.Abs(lgt) > 1)
                            {
                                MessageBox.Show("Giá trị tính toán không hợp lệ (sin > 1 hoặc < -1). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)Math.Asin(lgt);  // Radian
                            list.Items.Add("Công thức 1: α = asin(a * sinβ / b) => α = " + RagToDeg(value).ToString("0.#####") + "°");
                            break;
                        case 1:
                            // β = asin(b * sinα / a)  // Tính radian
                            if (a[3, 0] == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (cạnh a = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            lgt = (float)((a[4, 0] * Math.Sin(a[0, 0])) / (a[3, 0]));
                            if (Math.Abs(lgt) > 1)
                            {
                                MessageBox.Show("Giá trị tính toán không hợp lệ (sin > 1 hoặc < -1). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)Math.Asin(lgt);  // Radian
                            list.Items.Add("Công thức 1: β = asin(b * sinα / a) => β = " + RagToDeg(value).ToString("0.#####") + "°");
                            break;
                        case 3:
                            // a = b * sinα / sinβ
                            if (Math.Sin(a[1, 0]) == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (sinβ = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)((a[4, 0] * Math.Sin(a[0, 0])) / Math.Sin(a[1, 0]));
                            list.Items.Add("Công thức 1: a = b * sinα / sinβ => a = " + value.ToString());
                            break;
                        case 4:
                            // b = a * sinβ / sinα
                            if (Math.Sin(a[0, 0]) == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (sinα = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)((a[3, 0] * Math.Sin(a[1, 0])) / Math.Sin(a[0, 0]));
                            list.Items.Add("Công thức 1: b = a * sinβ / sinα => b = " + value.ToString());
                            break;
                    }
                    break;
                case 1:
                    switch (yeuto1)
                    {
                        case 1:
                            // sinβ = b * sinδ / c  => β = asin(b * sinδ / c)  // Tính radian
                            if (a[5, 1] == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (cạnh c = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            lgt = (float)((a[4, 0] * Math.Sin(a[2, 1])) / (a[5, 1]));
                            if (Math.Abs(lgt) > 1)
                            {
                                MessageBox.Show("Giá trị tính toán không hợp lệ (sin > 1 hoặc < -1). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)Math.Asin(lgt);  // Radian
                            list.Items.Add("Công thức 2: β = asin(b * sinδ / c) => β = " + RagToDeg(value).ToString("0.#####") + "°");
                            break;
                        case 2:
                            // sinδ = c * sinβ / b  => δ = asin(c * sinβ / b)  // Tính radian
                            if (a[4, 1] == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (cạnh b = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            lgt = (float)((a[5, 1] * Math.Sin(a[1, 1])) / a[4, 1]);
                            if (Math.Abs(lgt) > 1)
                            {
                                MessageBox.Show("Giá trị tính toán không hợp lệ (sin > 1 hoặc < -1). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)Math.Asin(lgt);  // Radian
                            list.Items.Add("Công thức 2: δ = asin(c * sinβ / b) => δ = " + RagToDeg(value).ToString("0.#####") + "°");
                            break;
                        case 4:
                            // b = c * sinβ / sinδ
                            if (Math.Sin(a[2, 1]) == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (sinδ = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)((a[5, 1] * Math.Sin(a[1, 0])) / Math.Sin(a[2, 1]));
                            list.Items.Add("Công thức 2: b = c * sinβ / sinδ => b = " + value.ToString());
                            break;
                        case 5:
                            // c = b * sinδ / sinβ
                            if (Math.Sin(a[1, 0]) == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (sinβ = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)((a[4, 0] * Math.Sin(a[2, 1])) / Math.Sin(a[1, 0]));
                            list.Items.Add("Công thức 2: c = b * sinδ / sinβ => c = " + value.ToString());
                            break;
                    }
                    break;
                case 2:
                    float chuvi = (float)((a[3, 0] + a[4, 0] + a[5, 1]) / 2f);
                    // p = (a + b + c) / 2
                    switch (yeuto1)
                    {
                        case 3:
                            // a = p - (S^2 / p*(p-b)*(p-c))
                            if (chuvi == 0 || (chuvi - a[4, 0]) == 0 || (chuvi - a[5, 1]) == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (chu vi hoặc hiệu = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            double tempA = Math.Pow(a[7, 2], 2.0) / (chuvi * (chuvi - a[4, 0]) * (chuvi - a[5, 1]));
                            if (tempA < 0)
                            {
                                MessageBox.Show("Không thể tính cạnh (giá trị âm trong sqrt). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)(chuvi - tempA);
                            list.Items.Add("Công thức 3: a = p - (S^2 / p*(p-b)*(p-c)) => a = " + value.ToString());
                            break;
                        case 4:
                            // b = p - (S^2 / p*(p-a)*(p-c))
                            if (chuvi == 0 || (chuvi - a[3, 0]) == 0 || (chuvi - a[5, 1]) == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (chu vi hoặc hiệu = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            double tempB = Math.Pow(a[7, 2], 2.0) / (chuvi * (chuvi - a[3, 0]) * (chuvi - a[5, 1]));
                            if (tempB < 0)
                            {
                                MessageBox.Show("Không thể tính cạnh (giá trị âm trong sqrt). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)(chuvi - tempB);
                            list.Items.Add("Công thức 3: b = p - (S^2 / p*(p-a)*(p-c)) => b = " + value.ToString());
                            break;
                        case 5:
                            // c = p - (S^2 / p*(p-a)*(p-b))
                            if (chuvi == 0 || (chuvi - a[3, 0]) == 0 || (chuvi - a[4, 1]) == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (chu vi hoặc hiệu = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            double tempC = Math.Pow(a[7, 2], 2.0) / (chuvi * (chuvi - a[3, 0]) * (chuvi - a[4, 1]));
                            if (tempC < 0)
                            {
                                MessageBox.Show("Không thể tính cạnh (giá trị âm trong sqrt). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)(chuvi - tempC);
                            list.Items.Add("Công thức 3: c = p - (S^2 / p*(p-a)*(p-b)) => c = " + value.ToString());
                            break;
                        case 6:
                            // p = (a + b + c) / 2
                            value = chuvi;
                            list.Items.Add("Công thức 3: p = (a + b + c) / 2 => p = " + value.ToString());
                            break;
                        case 7:
                            // S = sqrt(p(p-a)(p-b)(p-c))
                            double dienTich = chuvi * (chuvi - a[3, 0]) * (chuvi - a[4, 0]) * (chuvi - a[5, 1]);
                            if (dienTich < 0)
                            {
                                MessageBox.Show("Không thể tính diện tích (cạnh không thỏa bất đẳng thức tam giác).", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)Math.Sqrt(dienTich);
                            list.Items.Add("Công thức 3: S = sqrt(p(p-a)(p-b)(p-c)) => S = " + value.ToString());
                            break;
                    }
                    break;
                case 3:
                    switch (yeuto1)
                    {
                        case 0:
                            // α = π - β - δ  // Radian
                            value = (float)(Math.PI - a[1, 0] - a[2, 1]);
                            list.Items.Add("Công thức 4: α = 180 - β - δ => α = " + RagToDeg(value).ToString("0.#####") + "°");
                            break;
                        case 1:
                            // β = π - α - δ  // Radian
                            value = (float)(Math.PI - a[0, 0] - a[2, 1]);
                            list.Items.Add("Công thức 4: β = 180 - α - δ => β = " + RagToDeg(value).ToString("0.#####") + "°");
                            break;
                        case 2:
                            // δ = π - α - β  // Radian
                            value = (float)(Math.PI - a[0, 0] - a[1, 0]);
                            list.Items.Add("Công thức 4: δ = 180 - α - β => δ = " + RagToDeg(value).ToString("0.#####") + "°");
                            break;
                    }
                    break;
                case 4:
                    switch (yeuto1)
                    {
                        case 5:
                            // c = 2 * S / h
                            if (a[8, 4] == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (chiều cao h = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)(2f * a[7, 2] / a[8, 4]);
                            list.Items.Add("Công thức 5: c = 2 * S / h => c = " + value.ToString());
                            break;
                        case 7:
                            // S = (c * h) / 2  // Từ c = 2*S/h => S = (c*h)/2
                            value = (float)((a[5, 1] * a[8, 4]) / 2f);
                            list.Items.Add("Công thức 5: S = (c * h) / 2 => S = " + value.ToString());
                            break;
                        case 8:
                            // h = 2 * S / c
                            if (a[5, 1] == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0 (cạnh c = 0). Kiểm tra lại đầu vào.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                stop = 1;
                                return;
                            }
                            value = (float)(2f * a[7, 2] / a[5, 1]);
                            list.Items.Add("Công thức 5: h = 2 * S / c => h = " + value.ToString());
                            break;
                    }
                    break;
                case 5:
                    switch (yeuto1)
                    {
                        case 3:
                            // a = 2 * p - b - c
                            value = (float)(2f * a[6, 2] - a[4, 0] - a[5, 1]);
                            list.Items.Add("Công thức 6: a = 2 * p - b - c => a = " + value.ToString());
                            break;
                        case 4:
                            // b = 2 * p - a - c
                            value = (float)(2f * a[6, 2] - a[3, 0] - a[5, 1]);
                            list.Items.Add("Công thức 6: b = 2 * p - a - c => b = " + value.ToString());
                            break;
                        case 5:
                            // c = 2 * p - a - b
                            value = (float)(2f * a[6, 2] - a[3, 0] - a[4, 0]);
                            list.Items.Add("Công thức 6: c = 2 * p - a - b => c = " + value.ToString());
                            break;
                        case 6:
                            // p = (a + b + c) / 2
                            value = (float)((a[3, 0] + a[4, 0] + a[5, 1]) / 2f);
                            list.Items.Add("Công thức 6: p = (a + b + c) / 2 => p = " + value.ToString());
                            break;
                    }
                    break;
            }
            // Kiểm tra lỗi chung và gán giá trị
            if (value <= 0 || float.IsNaN(value))
            {
                MessageBox.Show("Các yếu tố nhập vào không hợp lệ!!. Vui lòng kiểm tra lại.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                stop = 1;
            }
            else
            {
                for (int i = 0; i < congthuc; i++)
                    if (a[yeuto1, i] == -1)
                    {
                        a[yeuto1, i] = value;
                        ArrLuu[yeuto1, i] = 1;
                    }
            }

        }

        //Xử lý giá trị truyền vào từ textbox
        private void LayGiaTri()
        {
            Arr();
            if (!string.IsNullOrEmpty(txtgoc_a.Text))
            {
                float num = ((float)Math.PI * float.Parse(txtgoc_a.Text)) / goc;
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[0, i] == -1f && this.a[0, i] != 0)
                    {
                        this.a[0, i] = num;
                        this.ArrLuu[0, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtgoc_b.Text))
            {
                float num1 = ((float)Math.PI * float.Parse(txtgoc_b.Text)) / goc;
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[1, i] == -1f && this.a[1, i] != 0)
                    {
                        this.a[1, i] = num1;
                        this.ArrLuu[1, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtgoc_c.Text))
            {
                float num2 = ((float)Math.PI * float.Parse(txtgoc_c.Text)) / goc;
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[2, i] == -1f && this.a[2, i] != 0)
                    {
                        this.a[2, i] = num2;
                        this.ArrLuu[2, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtcanh_a.Text))
            {
                float num3 = float.Parse(txtcanh_a.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[3, i] == -1f && this.a[3, i] != 0)
                    {
                        this.a[3, i] = num3;
                        this.ArrLuu[3, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtcanh_b.Text))
            {
                float num4 = float.Parse(txtcanh_b.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[4, i] == -1f && this.a[4, i] != 0)
                    {
                        this.a[4, i] = num4;
                        this.ArrLuu[4, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtcanh_c.Text))
            {
                float num5 = float.Parse(txtcanh_c.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[5, i] == -1f && this.a[5, i] != 0)
                    {
                        this.a[5, i] = num5;
                        this.ArrLuu[5, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtdientich.Text))
            {
                float num6 = float.Parse(txtdientich.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[7, i] == -1f && this.a[7, i] != 0)
                    {
                        this.a[7, i] = num6;
                        this.ArrLuu[7, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtchieucao.Text))
            {
                float num7 = float.Parse(txtchieucao.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[8, i] == -1f && this.a[8, i] != 0)
                    {
                        this.a[8, i] = num7;
                        this.ArrLuu[8, i] = 1;
                    }
                }
            }
        }

        //Tính toán
        private void Xuly()
        {
            flag = true;
            LayGiaTri();
            while (flag == true)
            {
                flag = false;
                for (int i = 0; i < congthuc; i++)
                {
                    int layyeuto = LayYeuTo(i);
                    if (layyeuto != -1)
                    {
                        if (stop == 1)
                            break;
                        Cochelantruyen(i, layyeuto);
                        flag = true;
                        if (Tinhgiatri())
                        {

                            HienThiKetQua();
                            flag = false;
                            break;
                        }
                    }
                }
            }
            if (!Tinhgiatri() && stop == 0)
                MessageBox.Show("- Không đủ yếu tố !.\n- Không thể tính kết quả trên mạng ngữ nghĩa đã xây dựng !!.\n- Vui Lòng Xem Trợ Giúp !!", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            HienThiDGV();
        }

        private void HienThiDGV()
        {
            dataBangSuyDien.Rows.Clear();
            if (dataBangSuyDien.RowCount == 0)
                dataBangSuyDien.Rows.Add(10);
            for (int i = 0; i < yeuto; i++)
            {
                for (int j = 0; j <= congthuc; j++)
                {
                    if (j == 0)
                    {
                        dataBangSuyDien[j, i].Value = YEUTO[i];
                        dataBangSuyDien[j, i].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataBangSuyDien[j, i].Value = ArrLuu[i, j - 1];
                        if (ArrLuu[i, j - 1] == -1)
                            dataBangSuyDien[j, i].Style.BackColor = Color.LightSkyBlue;
                        if (ArrLuu[i, j - 1] == 1)
                            dataBangSuyDien[j, i].Style.BackColor = Color.Yellow;
                    }
                }
            }
        }

        //Xử lý ngoại lệ. Điều kiện của một tam giác.
        private int NgoaiLe()
        {
            int kiemtra = 0;
            if (!string.IsNullOrEmpty(txtgoc_a.Text) && !string.IsNullOrEmpty(txtgoc_b.Text) && !string.IsNullOrEmpty(txtgoc_c.Text))
                if (float.Parse((txtgoc_a.Text)) + float.Parse((txtgoc_b.Text)) + float.Parse((txtgoc_c.Text)) != 180)
                    kiemtra = 1;

            if (!string.IsNullOrEmpty(txtcanh_a.Text) && !string.IsNullOrEmpty(txtcanh_b.Text) && !string.IsNullOrEmpty(txtcanh_c.Text))
                if ((float.Parse((txtcanh_a.Text)) + float.Parse((txtcanh_b.Text)) <= float.Parse((txtcanh_c.Text))) || (float.Parse((txtcanh_a.Text)) + float.Parse((txtcanh_c.Text)) <= float.Parse((txtcanh_b.Text))) || (float.Parse((txtcanh_b.Text)) + float.Parse((txtcanh_c.Text)) <= float.Parse((txtcanh_a.Text))))
                    kiemtra = 2;

            if (!string.IsNullOrEmpty(txtcanh_a.Text) && !string.IsNullOrEmpty(txtcanh_b.Text) && !string.IsNullOrEmpty(txtcanh_c.Text) && !string.IsNullOrEmpty(txtgoc_a.Text) && !string.IsNullOrEmpty(txtgoc_b.Text) && !string.IsNullOrEmpty(txtgoc_c.Text))
                if (float.Parse((txtcanh_a.Text)) < float.Parse((txtcanh_b.Text)) && float.Parse((txtgoc_a.Text)) > float.Parse((txtgoc_b.Text)) || float.Parse((txtcanh_a.Text)) < float.Parse((txtcanh_c.Text)) && float.Parse((txtgoc_a.Text)) > float.Parse((txtgoc_c.Text)) || float.Parse((txtcanh_b.Text)) < float.Parse((txtcanh_c.Text)) && float.Parse((txtgoc_b.Text)) > float.Parse((txtgoc_c.Text)))
                    kiemtra = 3;
            return kiemtra;
        }

        #endregion

        #region Textbox
        //Chỉ cho phép nhập số vào textbox
        private void Chinhapso(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && ((int)e.KeyChar != 46))
            {
                e.Handled = true;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))
                {
                    int ma = (int)e.KeyChar;
                    if ((ma == 26) || (ma == 24) || (ma == 3) || (ma == 22) || (ma == 1))
                        e.Handled = true;
                }
            }
        }

        private void goc_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }

        private void goc_b_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }

        private void goc_c_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }

        private void canh_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }

        private void canh_b_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }

        private void canh_c_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }

        // vẽ tam giác 
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            
            int width = pictureBox2.Width;
            int height = pictureBox2.Height;

           
            if (final_a > 0 && final_b > 0 && final_c > 0 &&
                (final_a + final_b > final_c && final_a + final_c > final_b && final_b + final_c > final_a))
            {
                VeTamGiac(e.Graphics, width, height);
            }
            else
            {
                VeTamGiacMau(e.Graphics, width, height);
            }
        }

        private void dientich_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }

        private void chieucao_KeyPress(object sender, KeyPressEventArgs e)
        {
            Chinhapso(e);
        }
        #endregion

        #region Button

        private void btnLamlai_Click(object sender, EventArgs e)
        {
            cbbgiatritinh.SelectedItem = cbbgiatritinh.Items[0];
            Arr();
            HienThiDGV();
            pictureBox2.Refresh();
            txtgoc_a.Text = txtgoc_b.Text = txtgoc_c.Text = string.Empty;
            txtcanh_a.Text = txtcanh_b.Text = txtcanh_c.Text = string.Empty;
            txtdientich.Text = txtchieucao.Text = string.Empty;
            txtketqua.Text = string.Empty;
            list.Items.Clear();

            
            txtketqua.Text = string.Empty;
            list.Items.Clear();

            
            final_a = final_b = final_c = 0;
            final_alpha = final_beta = final_delta = 0;

            
            pictureBox2.Invalidate();

            txtThoiGianXuLy.Clear();

        }

        private void btnTính_Click(object sender, EventArgs e)
        {
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            
            pictureBox2.Invalidate();
            stop = 0;
            txtketqua.Text = string.Empty;

            
            txtThoiGianXuLy.Text = string.Empty;


            if (cbbgiatritinh.SelectedItem == cbbgiatritinh.Items[0] || NgoaiLe() == 1 || NgoaiLe() == 2 || NgoaiLe() == 3)
            {

                stopwatch.Stop();

                if (cbbgiatritinh.SelectedItem == cbbgiatritinh.Items[0])
                    MessageBox.Show("Chưa chọn giá trị tính!!", "Cảnh Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (NgoaiLe() == 1 || NgoaiLe() == 2 || NgoaiLe() == 3)
                    MessageBox.Show("- Giá trị nhập vào không thỏa mãn điều kiện của 1 tam giác.\n- Vui lòng nhập lại!.", "Cảnh Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                Xuly();             
                stopwatch.Stop();             
                pictureBox2.Invalidate();                
                TimeSpan ts = stopwatch.Elapsed;
                // Định dạng thời gian thành mili giây
                string elapsedTime = ts.TotalMilliseconds.ToString("0.0000");             
                txtThoiGianXuLy.Text = $"{elapsedTime} ms";
            }
        }

        #endregion

        #region Xử Lý Vẽ
        private void pictureBoxVeTamGiac_Paint(object sender, PaintEventArgs e)
        {
            VeTamGiac(e.Graphics, pictureBox1.Width, pictureBox1.Height);
        }

        #endregion

        #region  Xác định loại tamm giác
        private string XacDinhLoaiTamGiac()
        {
          
            if (final_a <= 0 || final_b <= 0 || final_c <= 0 || final_alpha <= 0 || final_beta <= 0 || final_delta <= 0)
            {
                return "Chưa đủ thông số để xác định loại tam giác.";
            }

          
            const double epsilon = 0.01;
            const double GOC_VUONG = 90.0;
            const double GOC_DEU = 60.0;

            // --- KIỂM TRA ĐIỀU KIỆN VUÔNG ---
            bool isVuong = Math.Abs(final_alpha - GOC_VUONG) < epsilon ||
                           Math.Abs(final_beta - GOC_VUONG) < epsilon ||
                           Math.Abs(final_delta - GOC_VUONG) < epsilon;

            // --- KIỂM TRA ĐIỀU KIỆN CÂN VÀ ĐỀU ---
            bool isCan = Math.Abs(final_a - final_b) < epsilon ||
                         Math.Abs(final_b - final_c) < epsilon ||
                         Math.Abs(final_c - final_a) < epsilon;

            // Kiểm tra Đều
            bool isDeu = (Math.Abs(final_a - final_b) < epsilon && Math.Abs(final_b - final_c) < epsilon) ||
                         (Math.Abs(final_alpha - GOC_DEU) < epsilon && Math.Abs(final_beta - GOC_DEU) < epsilon);

            // --- KẾT HỢP VÀ TRẢ VỀ KẾT QUẢ ---

            if (isDeu)
            {
                return "Tam giác Đều";
            }

            if (isVuong && isCan)
            {
                return "Tam giác Vuông cân";
            }

            if (isVuong)
            {
                return "Tam giác Vuông";
            }

            if (isCan)
            {
                return "Tam giác Cân";
            }

            // Nếu không thuộc các trường hợp đặc biệt trên
            return "Tam giác Thường";
           
        }
        #endregion

        #region Tam giac mau
   
        private void VeTamGiacMau(Graphics g, int width, int height)
        {
            
            g.Clear(pictureBox2.BackColor);

            
            float padding = 40;

            PointF A = new PointF(padding, height - padding);
            PointF B = new PointF(width - padding, height - padding);
            PointF C = new PointF(width / 2, padding * 2);

            PointF[] vertices = { A, B, C };

           
            Pen pen = new Pen(Color.Blue, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; 
            g.DrawPolygon(pen, vertices);

            
            Font font = new Font("Arial", 10, FontStyle.Italic);
            g.DrawString("Hình mẫu (Chờ đủ 3 cạnh để vẽ lại)", font, Brushes.Gray, width / 2 - 100, height / 2);

            
            HienThiThongSoTrenHinh(g, A, B, C, font, Brushes.Gray, pen);
        }
        #endregion

        #region Hien thi thong so len hinh
   
        private void HienThiThongSoTrenHinh(Graphics g, PointF A, PointF B, PointF C, Font font, Brush brush, Pen pen)
        {
           
            Font fontSide = new Font("Arial", 10, FontStyle.Italic);
            Font fontVertex = font;
            Brush brushSide = brush;
            Brush brushAngle = Brushes.DarkRed;

           
            string str_c = $"c"; if (final_c > 0) str_c += $"={final_c:0.##}";
            string str_b = $"b"; if (final_b > 0) str_b += $"={final_b:0.##}";
            string str_a = $"a"; if (final_a > 0) str_a += $"={final_a:0.##}";

            
            string str_alpha_val = ""; if (final_alpha > 0) str_alpha_val = $"α={final_alpha:0.##}°";
            string str_beta_val = ""; if (final_beta > 0) str_beta_val = $"β={final_beta:0.##}°";
            string str_delta_val = ""; if (final_delta > 0) str_delta_val = $"δ={final_delta:0.##}°";

            
            g.DrawString(str_c, fontSide, brushSide, (A.X + B.X) / 2 - (str_c.Length * 3), A.Y - 20);
            g.DrawString(str_b, fontSide, brushSide, (A.X + C.X) / 2 - 35, (A.Y + C.Y) / 2 - 10);
            g.DrawString(str_a, fontSide, brushSide, (B.X + C.X) / 2 + 5, (B.Y + C.Y) / 2 - 10);

            
            g.DrawString($"A", fontVertex, brushSide, A.X - 15, A.Y + 5);
            if (final_alpha > 0) g.DrawString(str_alpha_val, fontVertex, brushAngle, A.X - 35, A.Y + 20);

            g.DrawString($"B", fontVertex, brushSide, B.X + 5, B.Y + 5);
            if (final_beta > 0) g.DrawString(str_beta_val, fontVertex, brushAngle, B.X + 15, B.Y + 20);

            g.DrawString($"C", fontVertex, brushSide, C.X - 5, C.Y - 20);
            if (final_delta > 0) g.DrawString(str_delta_val, fontVertex, brushAngle, C.X - 25, C.Y - 35);
        }
        #endregion

        #region Hien thi thoi gian xu ly
        private void HienThiThoiGianXuLy(TimeSpan ts)
        {         
            string elapsedTime = ts.TotalMilliseconds.ToString("0.000000");
            txtThoiGianXuLy.Text = $"{elapsedTime} ms";
        }
        #endregion
    }
}
