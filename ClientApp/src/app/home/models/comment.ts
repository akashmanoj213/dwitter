import { Content } from "@angular/compiler/src/render3/r3_ast";

export interface Comment {
    id?: number;
    content: string;
    postId: number;
    userId: number;
}