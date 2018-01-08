import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogEntryWithCommentsComponent } from './blog-entry-with-comments.component';

describe('BlogEntryWithCommentsComponent', () => {
  let component: BlogEntryWithCommentsComponent;
  let fixture: ComponentFixture<BlogEntryWithCommentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlogEntryWithCommentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogEntryWithCommentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
