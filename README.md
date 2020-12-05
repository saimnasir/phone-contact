# phone-contact

Telefon rehberi assignment olarak hazırlanmıştır.
.Net Core, MSSQL, Git kullanılarak geliştirmeler yapılmıştır.

# Amaçlar

1: Rehberde kişi oluşturma
2: Rehberde kişi kaldırma
3: Rehberdeki kişiye iletişim bilgisi ekleme
4: Rehberdeki kişiden iletişim bilgisi kaldırma
5: Rehberdeki kişilerin listelenmesi
6: Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi
7: Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi
8: Sistemin oluşturduğu raporların listelenmesi
9: Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi

# Mikro Servisler

Üç mikroservis içermektedir:
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

# [Id,UIID]
Id ve UIID ikilisi birlikte kullanılarak veri güvenliği sağlanmıştır. Girilen Id ile veri okunup okunan veri üzerindeki UIID ile girilen UIID'nin *eşitliği* kontrol edilerek eşitsizlik durumunda hata verilir.
