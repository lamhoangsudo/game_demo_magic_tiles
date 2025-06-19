HƯỚNG DẪN CHẠY DỰ ÁN
Yêu cầu:

Unity 2021.3 LTS trở lên

Newtonsoft.Json (cài qua NuGet hoặc Unity Package)

Cách chạy:

Mở project trong Unity

Đặt file map JSON vào thư mục StreamingAssets

Gắn NoteLoader vào GameObject trong scene

Gắn AudioSource và thiết lập bài nhạc tương ứng

Nhấn Play để bắt đầu game

GIẢI THÍCH THIẾT KẾ
Lối chơi: Người chơi chạm vào tile rơi xuống đúng lúc trùng với nhịp nhạc (clone theo Magic Tiles 3), thua khi người chơi để lọt một note combo được tính theo loại chạm tile (VD: nếu người chơi đang combo good x 3 thì khi chạm perfect combo được tính lại từ đầu).
có 3 sao: người chơi nhận được
1 sao khi người chơi trung bình chạm note toàn loại nomal
2 sao khi người chơi trung bình chạm note toàn loại good
3 sao khi người chơi full chạm note toàn loại perfect

Tạo map: xuất từ file json.

Lane (đường rơi): Có 4 lane cố định, tile sẽ spawn ngẫu nhiên có kiểm soát (không trùng lane liên tục).

Tính điểm: Dựa vào độ lệch thời gian so với note.time, chia thành Perfect, Good, và Miss.

Spawn tile: Dựa trên AudioSource.time và độ trễ spawn cố định để tile rơi trúng nhịp.

TÀI SẢN NGOÀI SỬ DỤNG
Newtonsoft.Json (dùng để đọc file JSON dạng mảng):

Nguồn: https://www.newtonsoft.com/json

Cài qua NuGet hoặc Unity Package

Âm nhạc:

Judas.mp3 – do người dùng cung cấp, bản quyền không xác định.

(Tuỳ chọn) Các asset khác từ Unity Asset Store:
Skymon Icon Pack Free: https://assetstore.unity.com/packages/2d/gui/icons/skymon-icon-pack-free-282424