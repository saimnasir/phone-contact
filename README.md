# phone-contact

Telefon rehberi assignment olarak hazırlanmıştır.
.Net Core, MSSQL, Git kullanılarak geliştirmeler yapılmıştır.

3 mikroservis içermektedir. 
1: PhoneContactAPI
2: ReportingAPI
3: APIGateway

PhoneContatAPI'de kişi tanımlayabilir, kişiye telefon,  email ve konum tipinde sınırsız sayıda iletişim bilgisi ekleyebilirsiniz. Sadece konum bilgisi tekil olarak tutulmaktadır.
ReportingAPI'de PhoneContact mikro servisinde bulunan kayıtlar için rapor oluşturabilir, görüntüleyebilirsiniz.
APIGateway mikro servisi üzerinden diğer iki servisin metotları çağrılmaktadır. Buradan rapor talebinde bulunup, hazırlanan raporları listeyebilirsiniz.

Arayüz uygulaması geliştirilmedi. (İleri süreçte geliştirmelere devam edeceğim)

# Başlıyoruz
1: Önce veri tabanı configurasyonlarını düzenleyiniz. 
2: ReportingHangfire adında veri tabanı oluşturunuz.
3: Solutionda yer alan 3 projeyi de StartUp'ta başlatınız. 
4: Şimdi Start diyeliriz. :)
5: DB Migration işlemi projelerin startında yapılır. Harici herhangi bir script çalıştırılmasına ihtiyaç yoktur.

# Süreç
Kişi/İletişim bilgisi eklemelerini APIGateway üzerine taşıma henüz yapılmadı bu sebeple ilgili tarayıcı sekmesinden veri girişi yapınız. (şimdilik :) )
Rapor talebi gateway üzerinden yapılmaktadır. Talep oluşturulup asenkron bir şekilde rapor hazırlanır.
Sistemde şu anda sadece bir çeşit rapor desteklenmektedir: Enlem, Boylam ve Çap girilerek bu çapın merkezi, girilen enlem ve boylam olacak şekilde taranan alanda bulunan kişi sayısı ve telefon numarası sayısı raporlanmaktadır. 
Raporun asenkron hazırlanması Hangfire ile yapılmaktadır. Hangfire recurring background job ile Talep Edildi ve Hatalı durumunda bulunan raporlar alınıp sırayla işlenir.
Raporların işlenmesi için ReportingAPI üzerinden PhoneContactAPI'ye rest API çaprısı yapılarak veriler çekilir ve kaydedilir.
APIGateway üzerinden raporlar, durumları gösterilerek listelenir.
APIGateway'de hazırlanması tamamlanmış raporun detaylarını görebilirsiniz.

# Hangfire (https://www.hangfire.io/)
Message Queue sistemlerini şimdiye kadar kullanmadığımdan raporların asenkron hazırlanması işini bu kütüphane ile yaptım.

# Swagger (https://swagger.io/)
Swagger kütüphanesi kullanılarak basit bir şekilde tüm CRUDL işlemlerinin yapılması sağlanmıştır. Arayüz geliştirmesi ileri süreçte yapılacaktır.

# Unit Testing
Yapılmadı.
