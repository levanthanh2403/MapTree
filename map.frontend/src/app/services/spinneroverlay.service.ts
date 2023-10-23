import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import { BehaviorSubject, delay } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class SpinnerOverlayService {
    private _loading = new BehaviorSubject<boolean>(false);
    public readonly loading$ = this._loading.asObservable().pipe(delay(1));

    constructor() { }

    setLoading(loading: boolean) {
        if (loading) this._loading.next(true);
        else this._loading.next(false);
    }
}