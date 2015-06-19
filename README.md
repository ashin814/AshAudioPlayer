# AshAudioPlayer
Unity用のBGM/SE再生ライブラリ
* BGM/SEの再生/停止
* BGMのフェードイン/フェードアウト
* シーン遷移時のBGMの自動再生/停止
などの機能をもった簡易的なライブラリです。

##Install
AshAudioPlayer/Assets/Scripts/AshAudio/* を自分のUnityプロジェクトにコピー

##Usage
音楽再生が行われる一番最初のシーンに空のオブジェクトを作り、AshAudio.csをAddComponentしてください。
次にAshAudioPlayer.csを編集します。Awakeに書かれているaudioSourceLists.Add ();を自分の環境にあわせて書き換えてください。
あとは、音楽を再生したいところでメソッドを実行するだけです。

```
//再生
AshAudioPlayer.instance.SoundResource ("音楽の管理名").Play ();
//停止
AshAudioPlayer.instance.SoundResource ("音楽の管理名").Stop ();
//ボリューム変更
AshAudioPlayer.instance.SoundResource ("音楽の管理名").SetVolume (1.0f);
```

##Example
ashin814/AshAudioPlayer 自体がサンプルプロジェクトとなっています。
シーンによるBGMの再生/停止、SEの再生などが確認できます。

