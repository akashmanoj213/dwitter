import { Injectable } from '@angular/core';
import { CommentModel } from '../models/comment-model';
import { Comment } from '../models/comment';
import { ApiService } from 'src/app/shared/services/api.service';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private api: ApiService<CommentModel>) { }

  createComment(comment: Comment): Observable<CommentModel> {
    return this.api.post('comments', comment);
  }

  getCommentsByPostId(postId: number): Observable<CommentModel[]> {
    return this.api.getAllById('comments/post', postId);
  }
}
