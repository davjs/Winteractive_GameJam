using System;
using System.Collections.Generic;

public class Observable<T> {
    public delegate void ValueChangedEvent(T newState);

    public event ValueChangedEvent Changed;
    private T _value;

    public Observable(T value) {
        _value = value;
    }

    public void Set(T newValue) {
        _value = newValue;
        if (Changed != null)
            Changed(_value);
    }

    private T Get() {
        return _value;
    }

    public static implicit operator T(Observable<T> observable) {
        return observable.Get();
    }

    //TODO: Memory leak
    public void When(T value, Action action) {
        Changed += state => {
            if (EqualityComparer<T>.Default.Equals(state, value)) {
                action();
            }
        };
    }
}