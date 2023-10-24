import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistLocationComponent } from './hist-location.component';

describe('HistLocationComponent', () => {
  let component: HistLocationComponent;
  let fixture: ComponentFixture<HistLocationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HistLocationComponent]
    });
    fixture = TestBed.createComponent(HistLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
