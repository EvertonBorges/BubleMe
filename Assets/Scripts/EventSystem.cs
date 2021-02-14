using System;
using UnityEngine;

public class EventSystem
{

    public struct Event
    {
        private Action callback;

        public void Add(Action callback) => this.callback += callback;

        public void Remove(Action callback) => this.callback -= callback;

        public void Notify() => callback?.Invoke();
    }

    public struct Event<T>
    {
        private Action<T> callback;

        public void Add(Action<T> callback) => this.callback += callback;

        public void Remove(Action<T> callback) => this.callback -= callback;

        public void Notify(T obj) => callback?.Invoke(obj);
    }

    public struct Event<T1, T2>
    {
        private Action<T1, T2> callback;

        public void Add(Action<T1, T2> callback) => this.callback += callback;

        public void Remove(Action<T1, T2> callback) => this.callback -= callback;

        public void Notify(T1 obj1, T2 obj2) => callback?.Invoke(obj1, obj2);
    }

    public struct Event<T1, T2, T3>
    {
        private Action<T1, T2, T3> callback;

        public void Add(Action<T1, T2, T3> callback) => this.callback += callback;

        public void Remove(Action<T1, T2, T3> callback) => this.callback -= callback;

        public void Notify(T1 obj1, T2 obj2, T3 obj3) => callback?.Invoke(obj1, obj2, obj3);
    }

    public struct Event<T1, T2, T3, T4>
    {
        private Action<T1, T2, T3, T4> callback;

        public void Add(Action<T1, T2, T3, T4> callback) => this.callback += callback;

        public void Remove(Action<T1, T2, T3, T4> callback) => this.callback -= callback;

        public void Notify(T1 obj1, T2 obj2, T3 obj3, T4 obj4) => callback?.Invoke(obj1, obj2, obj3, obj4);
    }

    public static Event<Sprite> GameController_AddItem;

    public static Event GameController_Pause;
    public static Event GameController_Unpause;

    public static Event GameController_GameStart;
    public static Event GameController_MainMenu;

    public static Event Scene_Gameplay;
    public static Event Scene_MainMenu;
    public static Event Scene_Quit;

    public static Event Sfx_PopBuble;
    public static Event Sfx_BooksFall;
    public static Event Sfx_ItemPick;

    public static Event Player_Death;


}
