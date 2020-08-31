import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PostService } from '../../services/post.service';
import { PostModel } from '../../models/post-model';
import { Post } from '../../models/post';
import { AuthService } from 'src/app/auth/services/auth.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  @Input() userId: number;
  
  postForm: FormGroup;
  posts: PostModel[] = [];

  constructor(private formBuilder: FormBuilder, private postService: PostService) { }

  ngOnInit() {
    this.postForm = this.formBuilder.group({
      content: ['', Validators.required]
    });
    this.postService.getAllPosts().subscribe(posts => {
      this.posts = posts;
    });
  }

  onSubmit() {
    let post: Post = {
      content: this.postForm.controls.content.value,
      userId: this.userId,
      likes: 0
    }

    this.postService.createPost(post).subscribe(post => {
      this.posts.unshift(post);
      this.postForm.reset();
    });
  }

  likePost(index: number) {
    let postModel = this.posts[index];

    let post: Post = {
      id: postModel.id,
      content: postModel.content,
      likes: postModel.likes + 1,
      userId: postModel.userId
    }

    this.postService.likePost(post).subscribe(response => {
      this.posts[index] = response;
    });
  }
}
