# NJOCheck

[![Build and Release](https://github.com/denpadokei/NJOCheck/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/NJOCheck/actions/workflows/build-and-release.yml)

レベル選択画面で Note Jump Offset (ジャンプ距離) を変更したとき、その値に対応する通知テキストをステージ上に表示して NJS の感覚を確認できる MOD です。

## 機能

- Note Jump Offset のドロップダウンを変更すると、通知テキストを即座に表示
- CLOSE (-0.5) / CLOSER (-0.25) / DEFAULT (0) / FURTHER (0.25) / FAR (0.5) ごとに色・大きさ・表示位置を定義
- Note Jump Duration Type が Static のときは STATIC 表示に切り替え
- プレイボタンの背景色も選択中のオフセットに合わせて変化 (DEFAULT は通常のグラデーション)
- 表示・非表示をオフセットごとの設定で切り替え可能

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.19.0 |
| 依存 MOD | BSIPA 4.1.3 / SiraUtil 3.0.0 |

## インストール

1. Beat Saber を 1.19.0 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/NJOCheck/releases) から `NJOCheck-0.1.2-bs1.19.0.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

レベル選択画面で曲を選び、プレイ設定パネルの Note Jump Offset を変更します。値ごとに決まった位置・大きさ・色のテキストが表示されるため、実際のジャンプ距離の違いを目で確認できます。ゲーム内の操作はこれだけで、追加の設定画面はありません。

## 設定

| 項目 | 型 | 既定値 | 説明 |
| --- | --- | --- | --- |
| CloseVisible | bool | true | CLOSE (-0.5) の通知を表示する |
| CloserVisible | bool | true | CLOSER (-0.25) の通知を表示する |
| DefaultVisible | bool | false | DEFAULT (0) の通知を表示する |
| FurtherVisible | bool | true | FURTHER (0.25) の通知を表示する |
| FarVisible | bool | true | FAR (0.5) の通知を表示する |
| StaticVisible | bool | false | Static (Note Jump Duration Type = Static) の通知を表示する |

設定は `UserData/NJOCheck.json` に保存されます。ゲーム内に設定画面はないため、このファイルを直接編集してください。

## ビルド

```powershell
git clone https://github.com/denpadokei/NJOCheck.git
cd NJOCheck
dotnet build .\NJOCheck\NJOCheck.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。既定では `..\Refs` フォルダが参照先になります。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT
