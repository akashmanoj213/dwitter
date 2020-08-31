import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services/api.service';
import { PostModel } from '../models/post-model';
import { Post } from '../models/post';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private api: ApiService<PostModel>) { }

  createPost(post: Post): Observable<PostModel> {
    return this.api.post('posts', post);
  }

  getPostsByUserId(userId: number): Observable<PostModel[]> {
    return this.api.getAllById('posts/user', userId);
  }

  getAllPosts(): Observable<PostModel[]> {
    return this.api.getAll('posts');
  }

  likePost(post: Post): Observable<PostModel> {
    return this.api.put('posts', post.id, post);
  }
}
