# wpf音乐播放器
## 简介
* 一款简单的音乐播放器，用来学习WPF中的各项功能
* 开发环境：`Visual Studio 2015 & .Net 4.5`
* 基于`System.Windows.Media.MediaPlayer`类实现
* 界面与功能上都较为简单，但wpf的相关知识的都有

## 功能
      支持常见音频文件的播放
      支持基本的播放控制：
       播放/暂停
       曲目切换
      支持的播放顺序：
       列表循环
       随机播放
      支持存储播放列表
      支持文件及文件夹操作
      支持控制进度
      支持常见的歌词文件
      
## 项目展示
![image](https://github.com/Foggydays/wpf-music-player/blob/master/GitHubImage/Image3.png)
      
## 感想
      开发伊始几乎没有遵循任何设计模式，只为实现其功能，代码混乱不堪
      待项目有了雏形之后，开始一步步的利用WPF的功能，改进代码，完善MVVM模式，完善Data Binding，构建UserControl。。。
      经历了由简单到臃肿，又从臃肿到精简的过程，深深的体会到，改进代码比实现功能困难的多
      有时一个结构上的改进，往往意味着大量代码的重构，对于我这个新手来说真是叫苦不迭
      以后如果要开始新的项目，设计好程序架构可谓重中之重
