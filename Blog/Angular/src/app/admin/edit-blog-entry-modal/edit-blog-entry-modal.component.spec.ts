import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBlogEntryModalComponent } from './edit-blog-entry-modal.component';

describe('EditBlogEntryModalComponent', () => {
  let component: EditBlogEntryModalComponent;
  let fixture: ComponentFixture<EditBlogEntryModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditBlogEntryModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditBlogEntryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
