import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountComponent  } from './account/account.component';
import { RegisterComponent  } from './account/register/register.component';
import { CommentListComponent } from './blog/comment-list/comment-list.component';
import { BlogListComponent } from './blog/blog-list/blog-list.component';
import { BlogEntryComponent } from './blog/blog-entry/blog-entry.component';
import { BlogEntryWithCommentsComponent } from './blog/blog-entry-with-comments/blog-entry-with-comments.component';
import { BlogManageComponent } from './admin/blog-manage/blog-manage.component';

const routes: Routes = [
  {
    path: '',
    component: BlogListComponent
  },
  {
    path: 'register/:email',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: AccountComponent
  },
  {
    path: 'comments/:urlFriendlyId',
    component: BlogEntryWithCommentsComponent
  },
  {
    path: 'admin/blog',
    component: BlogManageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }