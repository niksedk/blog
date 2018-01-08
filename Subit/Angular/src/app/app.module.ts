import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { RegisterComponent } from './account/register/register.component';
import { LoginComponent } from './account/login/login.component';
import { AccountService } from './account/account.service';
import { Http, HttpModule } from '@angular/http';
import { CommentListComponent } from './blog/comment-list/comment-list.component';
import { BlogService } from './blog/blog.service';
import { BlogListComponent } from './blog/blog-list/blog-list.component';
import { BlogEntryComponent } from './blog/blog-entry/blog-entry.component';
import { BlogEntryWithCommentsComponent } from './blog/blog-entry-with-comments/blog-entry-with-comments.component';
import { BlogManageComponent } from './admin/blog-manage/blog-manage.component';
import { UserManageComponent } from './admin/user-manage/user-manage.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    RegisterComponent,
    LoginComponent,
    CommentListComponent,
    BlogListComponent,
    BlogEntryComponent,
    BlogEntryWithCommentsComponent,
    BlogManageComponent,
    UserManageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpModule
  ],
  providers: [AccountService, BlogService],
  bootstrap: [AppComponent]
})
export class AppModule { }
