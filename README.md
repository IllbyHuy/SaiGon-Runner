# SaiGon Runner - Unity Game Project

## Mô tả
Dự án game runner được phát triển bằng Unity Engine.

## Yêu cầu hệ thống
- Unity Editor 6000.2.3f1 hoặc phiên bản mới hơn
- Windows 10/11

## Hướng dẫn cài đặt và chạy project

### 1. Clone repository
```bash
git clone https://github.com/IllbyHuy/SaiGon-Runner.git
cd SaiGon-Runner
```

### 2. Mở project trong Unity
1. Mở Unity Hub
2. Click "Open" và chọn thư mục project vừa clone
3. Unity sẽ tự động import các packages cần thiết

### 3. Cấu hình project (nếu cần)
- Đảm bảo Unity Editor version phù hợp (6000.2.3f1)
- Kiểm tra Package Manager để đảm bảo tất cả packages đã được cài đặt
- Nếu có lỗi, thử Refresh trong Package Manager

### 4. Chạy game
- Mở scene `Assets/Scenes/SampleScene.unity`
- Click Play button để chạy game

## Cấu trúc project
```
Assets/
├── Animation/          # Animations và controllers
├── Environment/        # Background và environment assets
├── Pretab/            # Prefabs
├── Resources/         # Game resources
├── Scenes/            # Game scenes
├── Scripts/           # C# scripts
└── Settings/          # Project settings
```

## Packages sử dụng
- Universal Render Pipeline (URP)
- 2D Animation
- Input System
- Cinemachine
- AI Tools

## Lưu ý quan trọng
- Không commit các file trong thư mục `UserSettings/`, `GeneratedAssets/`, `Library/`
- Luôn backup project trước khi thay đổi lớn
- Sử dụng Unity Version Control nếu làm việc nhóm

## Troubleshooting
Nếu gặp lỗi khi mở project:
1. Xóa thư mục `Library/` và mở lại Unity
2. Kiểm tra Unity Editor version
3. Refresh Package Manager
4. Reimport All Assets trong Unity

## Liên hệ
Tác giả: IllbyHuy
Repository: https://github.com/IllbyHuy/SaiGon-Runner.git
