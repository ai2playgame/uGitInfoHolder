# uGitInfoHolder

# 動作確認

- Unity 2021.3.9f1
- Windows 11
- Git for Windows

# Gitのhash値を取得する方法

```csharp
using UGitVersionHolder.Runtime;

string hash = GitVersion.GetHash();
```

# 最新コミットの日付を取得する方法
```csharp
string date = GitVersion.GetDate(); // 9月16日なら「0916」と帰ってくる
```

