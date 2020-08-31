import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/services/auth.service';
import { CommentModel } from '../../models/comment-model';
import { Comment } from '../../models/comment';
import { CommentService } from '../../services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() postId: number;
  @Input() userId: number;
  
  comments: CommentModel[] = [];
  commentForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private commentService: CommentService,
    private authService: AuthService) { 
      this.commentForm = this.formBuilder.group({
        content: ['', Validators.required]
      });
  }

  ngOnInit() {
    this.commentService.getCommentsByPostId(this.postId).subscribe(comments=>{
      this.comments = comments;
    });
  }

  onSubmit() {
    let comment: Comment = {
      content: this.commentForm.controls.content.value,
      postId: this.postId,
      userId: this.userId
    }
    
    this.commentService.createComment(comment).subscribe(comment => {
      this.comments.unshift(comment);
    });
  }

}
