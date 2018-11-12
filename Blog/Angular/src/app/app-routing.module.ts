import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent  } from './account/register/register.component';
import { BlogListComponent } from './blog/blog-list/blog-list.component';
import { BlogEntryWithCommentsComponent } from './blog/blog-entry-with-comments/blog-entry-with-comments.component';
import { BlogManageComponent } from './admin/blog-manage/blog-manage.component';
import { UserManageComponent } from './admin/user-manage/user-manage.component';
import { CommentManageComponent  } from './admin/comment-admin/comment-manage.component';
import { LoginComponent } from './account/login/login.component';
import { ReferrerManageComponent } from './admin/referrer-manage/referrer-manage.component';
import { GoogleLoginComponent } from './account/google-login/google-login.component';

const routes: Routes = [
  {
    path: '',
    component: BlogListComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'login/oauth2callback',
    component: GoogleLoginComponent
  },
  {
    path: 'blog/:urlFriendlyId',
    component: BlogEntryWithCommentsComponent
  },
  {
    path: 'admin/blog',
    component: BlogManageComponent
  },
  {
    path: 'admin/users',
    component: UserManageComponent
  },
  {
    path: 'admin/comments',
    component: CommentManageComponent
  },
  {
    path: 'admin/referrers',
    component: ReferrerManageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
