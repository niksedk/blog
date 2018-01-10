import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { BlogEntry } from './models/blog-entry';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { BlogEntryFull } from './models/blog-entry-full';
import { BlogComment } from './models/blog-comment';

@Injectable()
export class BlogService {
  private baseUrl = 'http://localhost:54882/api/blog'; // TODO: move to json setting

  constructor(private http: HttpClient) {}

  list(): Observable<BlogEntry[]> {
    return this.http.get<BlogEntry[]>(this.baseUrl);
  }

  getFullBlogEntry(friendlyId: string): Observable<BlogEntryFull> {
    return this.http.get<BlogEntryFull>(`${this.baseUrl}/${friendlyId}`);
  }

  deleteBlogEntry(blogEntryId: number) {
    return this.http.delete(`${this.baseUrl}/${blogEntryId}`);
  }

  addComment(comment: BlogComment): Observable<BlogComment> {
    return this.http.post<BlogComment>(`${this.baseUrl}/${comment.blogEntryId}/comments`, comment);
  }

  addBlogEntry(blogEntry: BlogEntry): Observable<BlogEntry> {
    return this.http.post<BlogEntry>(this.baseUrl, blogEntry);
  }

  deleteComment(commentId: number) {
    return this.http.delete(`${this.baseUrl}/comments/${commentId}`);
  }

  listComments(): Observable<BlogComment[]> {
    return this.http.get<BlogComment[]>(`${this.baseUrl}/comments`);
  }

}
