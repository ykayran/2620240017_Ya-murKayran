# 2620240017_YağmurKayran

**Ders:** DOT5105 - Oyun İçin Kod Yazımı (Vize Ödevi)
**Program:** Dijital Oyun Teknolojileri
**Oyun Motoru:** Unity 6 (6000.6.3f1) | **Dil:** C#

## Oyunun Kısa Açıklaması
Tek sahneden oluşan 3D bir top toplama oyunudur. Oyuncu bir küreyi WASD veya yön tuşlarıyla hareket ettirir, Space ile zıplar. Yeşil nesneler 10 puan kazandırır, kırmızı nesneler 5 puan eksiltir. Oyuncu 50 veya üzeri puana ulaştıktan sonra mavi bitiş alanına girerse sahne yeniden yüklenir ve oyun baştan başlar. Puan 50'nin altındaysa bitiş alanı oyunu sonlandırmaz. Oyun alanında zıplayarak geçilmesi gereken engeller bulunur.

## Kontroller
| Tuş | İşlev |
|---|---|
| W A S D / Yön tuşları | Hareket |
| Space | Zıplama (sadece zemindeyken) |

## Sahne Nesneleri
| Nesne | Tag | Açıklama |
|---|---|---|
| Player | - | Rigidbody'li küre, oyuncu |
| Ground | Ground | Zemin |
| Obstacle | Ground | Zıplanması gereken engeller |
| Green Item | Green | +10 puan (Is Trigger açık) |
| Red Item | Red | -5 puan (Is Trigger açık) |
| End | Bitiş alanı (Is Trigger açık) |

## Scriptler
- **PlayerController.cs:** Oyunun tüm mantığını içerir. Oyuncunun hareketi, zıplaması, zemin kontrolü, nesne toplama, puan hesabı, bitiş kontrolü, sahneyi yeniden başlatma ve puanın ekranda gösterilmesi bu script ile yapılır. Player nesnesine eklenmiştir.
- **CameraFollow.cs:** Main Camera'nın oyuncuyu sabit mesafeden takip etmesini sağlar. 

## Kullanılan Yapılar
Değişkenler (int, float, bool), if / else, Update(), FixedUpdate(), GetComponent(), Rigidbody, AddForce(), isGrounded, OnTriggerEnter(), CompareTag(), Destroy(), SceneManager.

## Algoritma: isGrounded ile Zıplama
1. `isGrounded` adlı bool değişken oyuncunun zeminde olup olmadığını tutar.
2. `OnCollisionStay()` metodunda oyuncunun çarptığı nesne `CompareTag("Ground")` ile kontrol edilir. Zemin etiketliyse `isGrounded = true` olur.
3. `Update()` içinde her karede Space tuşuna basılıp basılmadığına bakılır. Koşul `if (Input.GetKeyDown(KeyCode.Space) && isGrounded)` şeklindedir.
4. Koşul doğruysa `rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse)` ile yukarı yönde ani kuvvet uygulanır ve `isGrounded = false` yapılır.
5. Oyuncu zeminden ayrılınca `OnCollisionExit()` de `isGrounded` değerini false yapar. Böylece havadayken ikinci kez zıplanamaz.

Hareket ise `FixedUpdate()` içinde `rb.AddForce(movement * speed)` ile fizik tabanlı olarak yapılır. `speed` ve `jumpForce` public olduğu için Inspector'dan değiştirilebilir.

## Hata Analizi
**Hata 1:** Oyun başladığında oyuncu hareket etmedi, Console'da `InvalidOperationException: You are trying to read Input using the UnityEngine.Input class, but you have switched active input handling to Input System package` hatası çıktı.
**Çözüm:** Unity 6 yeni projelerde yeni Input System ile geliyor, kodum ise eski `Input.GetAxis` sistemini kullanıyordu. Edit > Project Settings > Player > Other Settings > Active Input Handling ayarını **Both** yaptım ve editörü yeniden başlattım. Hareket çalışmaya başladı.

**Hata 2:** Oyuncu zeminin kenarından düşüp geri dönemiyordu.
**Çözüm:** Zeminin kenarlarına duvarlar ekledim. Ayrıca oyuncu `y < -10` konumuna düşerse `RestartGame()` metoduyla sahnenin yeniden başlaması için kontrol ekledim.

**Test edilen durum:** 50 puandan az puanla bitiş alanına girildiğinde oyunun sonlanmadığı, 50 ve üzeri puanla girildiğinde sahnenin yeniden yüklendiği test edildi.
