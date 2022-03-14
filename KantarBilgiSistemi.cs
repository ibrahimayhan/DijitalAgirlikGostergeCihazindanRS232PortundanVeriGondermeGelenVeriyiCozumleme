//     SerialPortKBS            =   System.IO.Ports.SerialPort İle Toolbox Üzerinden Dahil Edilen SerialPort Nesnesi
//     Önemli Bilgilendirme     =   Dijital Ağırlık Göstergesi Cihazınız Üzerinde Veri Modu Sor / Al Şeklinde Olması Gerekmektedir,
//                                  Cihazlar Varsayılan Olarak Seri Veri Gönderme Modundadır

//                                  BTNIlkAgirlik_Click, KantarBilgiSistemi_FormClosed, SerialPortKBS_DataReceived Form Üzerine Event Tanımlanması Gerekmektedir

private void BTNIlkAgirlik_Click(object sender, EventArgs e)
{
    SerialPortKBS.Open();
    string Agirlik;
    string OkunanVeri = "";

    // DiscardInBuffer ve DiscardOutBuffer İle SerialPort Üzerinde Cache Temizleme İşlemi Uygulanması
    SerialPortKBS.DiscardInBuffer();
    SerialPortKBS.DiscardOutBuffer();

    while (OkunanVeri == "")
    {
        VeriTalepGonder();
    }

    // OkunanVeri while Döngüsü Sonrasında SerialPort İle İşlem Bittiği İçin Close İle Kapatılması
    SerialPortKBS.Close();
}

private void KantarBilgiSistemi_FormClosed(object sender, FormClosedEventArgs e)
{
    // SerialPort Bağlantı Açıksa Form Kapatma İşleminde Bağlantının Kapatılması

    if (SerialPortKBS.IsOpen == true)
    {
        SerialPortKBS.Close();
    }
}

private void VeriTalepGonder()
{
    try
    {
        // Burada Write Yapılan Char Değerler Dijital Ağırlık Gösterge Cihazları Arasında Farklılık Gösterebilmektedir
        // Kullandığınız Gösterge Cihazına Göre Gönderilecek Olan Char ve String Değer Değerlerini Öğrenmeniz Gerekmektedir

        char CHR = Convert.ToChar(2);
        string STRG1 = CHR.ToString();
        CHR = Convert.ToChar(Convert.ToInt32("1"));
        string STRG2 = CHR.ToString();
        string STRG3 = "DNG";
        CHR = Convert.ToChar(13);
        string STRG4 = CHR.ToString();
        SerialPortKBS.Write(STRG1 + STRG2 + STRG3 + STRG4);
    }
    catch (Exception EX)
    {
        MesssageBox.Show("Bir Hata Oluştu : " + EX);
        return;
    }
}

private void SerialPortKBS_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
{
    try
    {
        if (!SerialPortKBS.IsOpen || SerialPortKBS.BytesToRead < 41)
            return;
        OkunanVeri = SerialPortKBS.ReadExisting();
    }
    catch (Exception EX)
    {
        MesssageBox.Show("Bir Hata Oluştu : " + EX);
        return;
    }
}