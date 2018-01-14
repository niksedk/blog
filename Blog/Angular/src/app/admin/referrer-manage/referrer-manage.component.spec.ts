import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferrerManageComponent } from './referrer-manage.component';

describe('ReferrerManageComponent', () => {
  let component: ReferrerManageComponent;
  let fixture: ComponentFixture<ReferrerManageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReferrerManageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferrerManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
