using System;
using System.Collections.Generic;
using System.Linq;

namespace TahmazSualCavabOyunu
{
    class Program
    {
        static List<ImtahanSual> imtahanlar = new List<ImtahanSual>();
        static bool isAdmin = AdminGirisi();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Zəhmət olmasa seçim edin:");
                Console.WriteLine("1. Yeni sual əlavə etmək");
                Console.WriteLine("2. Bütün sualları göstərmək");
                if (!isAdmin)
                {
                    Console.WriteLine("3. Sınağı başlatmaq");
                }
                else
                {
                    Console.WriteLine("3. Sınaq əlavə etmək");
                    Console.WriteLine("4. Mövcud imtahanlarin siyahısını göstərmək");
                    Console.WriteLine("5. Suallari düzəltmək və ya silmək");
                }
                Console.WriteLine("6. Proqramın İstifadə qaydalari və Haqqında Məlumat");
                Console.WriteLine("7. Proqramı bağlamaq");
               

                if (!int.TryParse(Console.ReadLine(), out int Secim75))
                {
                    Console.WriteLine("Yalnış seçim. Zəhmət olmasa yenidən cəhd edin.");
                    continue;
                }

                try
                {
                    switch (Secim75)
                    {
                        case 1:
                            SualElaveEt();
                            break;
                        case 2:
                            SualariGoster();
                            break;
                        case 3:
                            if (!isAdmin)
                            {
                                İmtahani_Baslat();
                            }
                            else
                            {
                                İmtahan_Elave_Et();
                            }
                            break;
                        case 4:
                            if (isAdmin)
                            {
                                Imtahan_Sual_Sayini_Goster();
                            }
                            break;
                        case 5:
                            if (isAdmin)
                            {
                                suallari_elave_et_ve_sil();
                            }
                            break;
                        case 6:
                            HaqqindaMelumat();
                            break;
                        case 7:
                            Console.WriteLine("  Proqramdan istifadə etdiyiniz üçün təşəkkür edirik! \n Programin bağlanmasi üçün hər hansisa bir klavişə toxunun");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        
                        default:
                            Console.WriteLine("Yalnış seçim. Zəhmət olmasa yenidən cəhd edin.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Xəta baş verdi: " + ex.Message);
                    Console.WriteLine("Xəta izi (StackTrace): " + ex.StackTrace);
                }
            }
        }
        
        static bool AdminGirisi()
        {
            Console.Write("Giriş tipini seçin (Admin/İstifadəçi): ");
            string girisTipi = Console.ReadLine().ToLower();
            if (girisTipi == "admin")
            {
                Console.WriteLine("Admin girişi müvəffəqiyyətlə tamamlandı.");
                return true;
            }
            else if (girisTipi == "istifadeci" || girisTipi == "istifadəçi")
            {
                Console.WriteLine("İstifadəçi girişi müvəffəqiyyətlə tamamlandı.");
                return false;
            }
            else
            {
                Console.WriteLine("Yalnış giriş tipi. Proqram bağlanır.");
                Console.ReadKey();
                Environment.Exit(0);
                return false;
            }
        }

        static void SualElaveEt()
        {
            try
            {
                Console.WriteLine("Sualın növünü seçin (1 açıq və ya 2 qapalı):");
                string sualNovu = Console.ReadLine();

                if (sualNovu == "1")
                {
                    Console.WriteLine("Sualın mətnini daxil edin:");
                    string soruMetni = Console.ReadLine();

                    Console.WriteLine("Düzgün cavabı daxil edin:");
                    string duzgunCavab = Console.ReadLine();

                    Sual sual = new Sual(soruMetni, new List<string> { duzgunCavab });
                    ImtahanSual yeniSual = new ImtahanSual(new List<Sual> { sual });
                    imtahanlar.Add(yeniSual);
                    Console.WriteLine("Sual uğurla əlavə edildi.");
                }
                else if (sualNovu == "2")
                {
                    Console.WriteLine("Sualın mətnini daxil edin:");
                    string sualMetni = Console.ReadLine();

                    List<string> cavablar = new List<string>();
                    for (int i = 1; i <= 4; i++)
                    {
                        Console.WriteLine($"Variant {i}:");
                        cavablar.Add(Console.ReadLine());
                    }

                    Console.WriteLine("Düzgün cavabı seçin (1, 2, 3, 4):");
                    if (int.TryParse(Console.ReadLine(), out int duzgunVariant) && duzgunVariant >= 1 && duzgunVariant <= 4)
                    {
                        cavablar[duzgunVariant - 1] += "+";
                    }
                    else
                    {
                        Console.WriteLine("Yanlış variant seçimi.");
                    }

                    Sual sual = new Sual(sualMetni, cavablar);
                    ImtahanSual yeniSual = new ImtahanSual(new List<Sual> { sual });
                    imtahanlar.Add(yeniSual);
                    Console.WriteLine("Sual uğurla əlavə edildi.");
                }
                else
                {
                    Console.WriteLine("Yalnış sual növü seçdiniz. Zəhmət olmasa \"1\" və ya \"2\" daxil edin.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sual əlavə edilərkən xəta baş verdi: " + ex.Message);
                Console.WriteLine("Xəta izi (StackTrace): " + ex.StackTrace);
            }
        }
        static void HaqqindaMelumat()
        {
            Console.WriteLine("   Programin adi     : Təhməz Sual Cavab Oyunu");
            Console.WriteLine("   Müəllif           : Muradov Tahmaz");
            Console.WriteLine("   Versiya           : 85");
            Console.WriteLine("   Haqqında Məlumat  : Bu proqram sual və cavab oyunlarını idarə etmək və imtahanlar hazırlamaq üçün nəzərdə tutulub. \n ");
            Console.WriteLine("   Qaydalar          : \r\n\r\n# " + "Tahmaz Sual Cavab Oyunu İstifadə Qaydaları\r\n\r\n" +
                "**Proqram Haqqında:**\r\nBu proqram, sual və cavab oyunlarını idarə etmək və imtahanlar hazırlamaq üçün nəzərdə tutulub.\r\n\r\n" +
                "**Giriş:**\r\nProqramı başladıqda, istifadəçilərdən giriş tipini (Admin və ya İstifadəçi) seçmələri tələb olunur. Admin girişi ilə proqramın idarə olunmasına imkan verilir.\r\n\r\n## " +
                "Admin Girişi:\r\n\r\n- Admin kimi giriş edən şəxs proqramın bütün funksiyalarından istifadə edə bilər, yeni imtahanlar əlavə edə, sualları düzəltə və ya silə bilər.\r\n- " +
                "İmtahanlar və sualların idarə edilməsi üçün admin giriş tipini seçin.\r\n\r\n" +
                "## İstifadəçi Girişi:\r\n\r\n- İstifadəçi kimi giriş edən şəxs imtahanlar üçün sualları görmək və imtahanı başlatmaq imkanına sahib olur.\r\n- " +
                "İstifadəçi giriş tipini seçin.\r\n\r\n## Əsas Menu:\r\n\r\nProqramın əsas menu hissəsində aşağıdakı əməliyyatları yerinə yetirə bilərsiniz:\r\n\r\n1. " +
                "**Yeni Sual Əlavə Et:**\r\n   - Yeni bir sual əlavə etmək üçün bu seçimi seçin.\r\n  " +
                " - Açıq və ya qapalı suallar yarada bilərsiniz.\r\n   - Sual metnini, variantları və düzgün cavabı daxil edin.\r\n\r\n2. " +
                "**Bütün Sualları Göstər:**\r\n   - Əlavə edilmiş bütün sualları görmək üçün bu seçimi seçin.\r\n\r\n3. **Sınağı Başlatmaq (Yalnız İstifadəçi üçün):**\r\n   " +
                "- İstifadəçi kimi giriş edən şəxs imtahanı başlatmaq üçün bu seçimi seçin.\r\n   - İmtahan başladıqda sualların cavabını verin və nəticəni görün.\r\n\r\n4. " +
                "**İmtahan Əlavə Et (Yalnız Admin üçün):**\r\n   - Admin kimi giriş edən şəxs yeni bir imtahan əlavə etmək üçün bu seçimi seçin.\r\n   " +
                "- İmtahanın adını, tələb olunan sualları əlavə edin.\r\n\r\n5. **Mövcud İmtahanların Siyahısını Göstər (Yalnız Admin üçün):**\r\n  " +
                " - Admin kimi giriş edən şəxs bütün mövcud imtahanların siyahısını görüntüləmək üçün bu seçimi seçin.\r\n\r\n6. " +
                "**Sualları Düzəltmək və ya Silmək (Yalnız Admin üçün):**\r\n   " +
                "- Admin kimi giriş edən şəxs mövcud sualları düzəltmək və ya silmək üçün bu seçimi seçin.\r\n\r\n" +
                "7. **Proqramı Bağlamaq:**\r\n   - Proqramı bağlamaq və çıxmaq üçün bu seçimi seçin.\r\n\r\n8. **Proqramın İstifadə Qaydaları və Haqqında Məlumat:**\r\n   " +
                "- Proqramın istifadə qaydaları və haqqında məlumatı görmək üçün bu seçimi seçin.\r\n\r\n## Proqramın İstifadəsi:\r\n\r\n" +
                "- Qaydaları və haqqında məlumatı görüntülmək üçün \"Proqramın İstifadə Qaydaları və Haqqında Məlumat\" seçimini seçin.\r\n- " +
                "Giriş tiplərini və funksiyaları düzgün şəkildə seçin və istifadəçilərinizin proqramı düzgün şəkildə istifadə etməsinə nail olun.\r\n\r\n" +
                "\n                            OXUDUĞUNUZ ÜÇÜN TƏŞƏKKÜR EDIRƏM !!! \n \n");
            Console.WriteLine("" +
                "[assembly   :    AssemblyTitle(\"tahmaz_sual_cavab_oyunu\")]\r\n" +
                "[assembly   :    AssemblyDescription(\"Muradoff_Code\")]\r\n" +
                "[assembly   :    AssemblyConfiguration(\"\")]\r\n" +
                "[assembly   :    AssemblyCompany(\"Muradoff_Code\")]\r\n" +
                "[assembly   :    AssemblyProduct(\"tahmaz_sual_cavab_oyunu\")]\r\n" +
                "[assembly   :    AssemblyCopyright(\"Copyright ©  2023\")]\r\n" +
                "[assembly   :    AssemblyTrademark(\"Muradoff_Code\")]\r\n" +
                "[assembly   :    AssemblyCulture(\")]\r\n" +
                "[assembly   :    AssemblyVersion(\"1.0.*\")]\r\n" +
                "[assembly   :    AssemblyVersion(\"8.5.8.5\")]\r\n" +
                "[assembly   :    AssemblyFileVersion(\"7.5.7.5\")]\r\n" +
                "[assembly   :    NeutralResourcesLanguage(\"az\")]" +
                "\n" +
                "\n");
                
        }

        static void SualariGoster()
        {
            if (imtahanlar.Count == 0)
            {
                Console.WriteLine("Hələ heç bir sual əlavə edilməməişdir.");
            }
            else
            {
                Console.WriteLine("Sualar:");
                for (int i = 0; i < imtahanlar.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Sual: {imtahanlar[i].Suals[0].SualMetni}");
                }
            }
            Console.ReadKey();
        }

        static void İmtahani_Baslat()
        {
            try
            {
                if (imtahanlar.Count == 0)
                {
                    Console.WriteLine("İmtahana başlamaq üçün əvvəlcə suallar əlavə etməlisiniz.");
                    return;
                }

                ImtahanSual imtahan_teze = new ImtahanSual();
                foreach (ImtahanSual Ksq_bsq in imtahanlar)
                {
                    imtahan_teze.Suals.Add(new Sual(Ksq_bsq.Suals[0].SualMetni, new List<string>(Ksq_bsq.Suals[0].Cavablar)));
                }

                imtahan_teze.imtahan_baslama_zamani = DateTime.Now;
                imtahan_teze.imtahan_bitme_zamani = imtahan_teze.imtahan_baslama_zamani.AddMinutes(30);

                int duzgun_sual_sayi = 0;
                foreach (Sual sual_85 in imtahan_teze.Suals)
                {
                    Console.WriteLine("Sual: " + sual_85.SualMetni);
                    for (int i = 0; i < sual_85.Cavablar.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. Cavab: {sual_85.Cavablar[i]}");
                    }

                    Console.Write("Sizin cavabınızı seçin (1, 2, 3, ...): ");
                    if (int.TryParse(Console.ReadLine(), out int secilmisCavab))
                    {
                        secilmisCavab--;
                        if (secilmisCavab >= 0 && secilmisCavab < sual_85.Cavablar.Count &&
                            sual_85.Cavablar[secilmisCavab].EndsWith("+"))
                        {
                            Console.WriteLine("Düzgün cavab!");
                            duzgun_sual_sayi++;
                        }
                        else
                        {
                            Console.WriteLine("Səhv cavab.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Düzgün seçim edin !  Cavab seçiminiz sayı olmalıdır.");
                    }
                }

                imtahan_teze.imtahan_bitme_zamani = DateTime.Now;
                imtahan_teze.Duzgun_Cavablar = duzgun_sual_sayi;
                imtahan_teze.Toplam_Sual_Sayi = imtahan_teze.Suals.Count;

                Console.WriteLine($"Sınaq bitdi. Ümumi düzgün cavablar sayı: {duzgun_sual_sayi} / {imtahan_teze.Toplam_Sual_Sayi}");

                imtahanlar.Add(imtahan_teze);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sual əlavə edilərkən xəta baş verdi: " + ex.Message);
                Console.WriteLine("Xəta izi (StackTrace): " + ex.StackTrace);
            }
        }

        static void İmtahan_Elave_Et()
        {
            Console.WriteLine("Sınaqın adını daxil edin:");
            string imtahan_adi_bsq = Console.ReadLine();

            List<Sual> suallar = new List<Sual>();
            do
            {
                SualElaveEt();
                Console.WriteLine("Başqa sual əlavə etmək istəyirsiniz? (Y/N)");
            } while (Console.ReadLine().ToLower() == "y");

            ImtahanSual imtahanSual = new ImtahanSual();
            imtahanSual.Ad = imtahan_adi_bsq;
            imtahanSual.Suals = suallar;
            imtahanlar.Add(imtahanSual);
            Console.WriteLine("imtahan uğurla əlavə edildi ! ");
        }

        static void Imtahan_Sual_Sayini_Goster()
        {
            if (imtahanlar.Count == 0)
            {
                Console.WriteLine("Hələ heç bir imtahan əlavə edilməmişdir.");
            }
            else
            {
                Console.WriteLine("İmtahanlar : ");
                for (int i = 0; i < imtahanlar.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. İmtahan adı: {imtahanlar[i].Ad}, Başlama tarixi: {imtahanlar[i].imtahan_baslama_zamani}, Bitmə tarixi: {imtahanlar[i].imtahan_bitme_zamani}, Düzgün cavablar: {imtahanlar[i].Duzgun_Cavablar} / {imtahanlar[i].Toplam_Sual_Sayi}");
                }
            }
        }

        static void suallari_elave_et_ve_sil()
        {
            if (imtahanlar.Count == 0)
            {
                Console.WriteLine("Hələ heç bir sual əlavə edilməyib.");
                return;
            }
            Console.WriteLine("Düzətmək veya silmək istediyiniz sualin nömrəsini seçin:");
            for (int i = 0; i < imtahanlar.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Sual: {imtahanlar[i].Suals[0].SualMetni}");
            }

            if (int.TryParse(Console.ReadLine(), out int secilenSoruNo) && secilenSoruNo >= 1 && secilenSoruNo <= imtahanlar.Count)
            {
                Console.WriteLine("Sualı düzəltmək və ya silmək istəyirsiniz? (D/S)");
                string secim_85 = Console.ReadLine().ToLower();

                if (secim_85 == "d")
                {
                 
                    Sual secilenSual = imtahanlar[secilenSoruNo - 1].Suals[0];
                    Console.WriteLine("Sualın yenilənmiş mətnini daxil edin:");
                    secilenSual.SualMetni = Console.ReadLine();

                    Console.WriteLine("Düzgün cavabı yeniləyin:");
                    secilenSual.Cavablar[0] = Console.ReadLine();

                    Console.WriteLine("Sual uğurla yeniləndi.");
                }
                else if (secim_85 == "s")
                {
                    imtahanlar.RemoveAt(secilenSoruNo - 1);
                    Console.WriteLine("Sual uğurla silindi.");
                }
                else
                {
                    Console.WriteLine("Yalnış seçim. Zəhmət olmasa 'D' və ya 'S' daxil edin.");
                }
            }
            else
            {
                Console.WriteLine("Yalnış sual nömrəsi seçdiniz.");
            }
        }
    }

}

class Sual
{
    public string SualMetni { get; set; }
    public List<string> Cavablar { get; set; }

    public Sual(string sualMetni, List<string> cavablar)
    {
        SualMetni = sualMetni;
        Cavablar = cavablar;
    }
}

class ImtahanSual
{
    public string Ad { get; set; }
    public List<Sual> Suals { get; set; }
    public DateTime imtahan_baslama_zamani { get; set; }
    public DateTime imtahan_bitme_zamani { get; set; }
    public int Duzgun_Cavablar { get; set; }
    public int Toplam_Sual_Sayi { get; set; }

    public ImtahanSual(List<Sual> butun_sualar_s)
    {
        Ad = "";
        Suals = new List<Sual>();
        imtahan_baslama_zamani = DateTime.MinValue;
        imtahan_bitme_zamani = DateTime.MinValue;
        Duzgun_Cavablar = 0;
        Toplam_Sual_Sayi = 0;
    }

    public ImtahanSual()
    {

    }

}
