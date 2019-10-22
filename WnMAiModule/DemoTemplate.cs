using System.Collections.Generic;
using WnMAiModuleLib;

namespace WnMAiModule
{
    public class DemoTemplate : IStateTemplate
    {
        public Dictionary<string, WsState> States { get; set; } = new Dictionary<string, WsState>();
        
        public DemoTemplate()
        {
            //开始状态
            States.Add("start", new WsState().SetCollector((infos, data) =>
            {
                var avatar = data as TestAvatar;
            }).SetAction((infos, step, data) =>
            {
                var avatar = data as TestAvatar;
                avatar.MoveTo(avatar.Side == 0?200:600-16, 50 + avatar.Position*70);
            }).AddJudge("prepare", (infos, parent, data) =>
            {
                var avatar = data as TestAvatar;
                return avatar.MoveEnd;
            }));
            //准备状态
            States.Add("prepare", new WsState().SetInit(((infos, data) =>
            {
                infos.TalkTimer = 1f;//垃圾话
            })).SetCollector(((infos, data) =>
            {
                var avatar = data as TestAvatar;
                if (infos.TalkTimer > 0)
                {
                    infos.TalkTimer -= Tester.TimeStep; //计时
                }
            })).SetAction(((infos, step, data) =>
            {
                var avatar = data as TestAvatar;
                if (infos.TalkTimer <= 0)
                {
                    string[] trashTalk = {"来，搞起","康忙北鼻","单挑啊",$"对面的{avatar.Position}号你完了" };
                    avatar.Talk(trashTalk[Tester.Rnd.Next(0, trashTalk.Length)]);
                    infos.TalkTimer = 5f;
                }
            })));
        }
    }
}
