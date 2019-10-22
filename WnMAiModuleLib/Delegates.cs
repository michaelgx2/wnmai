namespace WnMAiModuleLib
{
    public delegate void DelInitState(dynamic infos, object data);
    public delegate void DelCollectInfo(dynamic infos, object data);
    public delegate void DelAction(dynamic infos, float timeStep, object data);
    public delegate bool DelJudge(dynamic infos, WsMachine parent, object data);
}