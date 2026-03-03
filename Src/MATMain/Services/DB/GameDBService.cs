using System.Diagnostics;

namespace MATMain.Services.DB;

public static partial class DBService
{
    public static string GetGameInfo(string gamename)
    {
        Debug.Assert(string.IsNullOrEmpty(gamename));

        return $"select * from games where name = {gamename}";
    }

    public static string InsertGameInfo(string name, string tabticker, int type)
    {
        // text column : datetime('now', 'localtime') // 로컬 컴 기준 시간
        return $"insert into games (name, tabticker, playertype, createtime, updatetime) " +
            $" values('{name}','{tabticker}', {type}, datetime('now', 'localtime'), datetime('now', 'localtime') );";
    }
}