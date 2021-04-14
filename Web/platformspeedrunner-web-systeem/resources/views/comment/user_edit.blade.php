@extends('layouts.app')

@section('title')
    Edit comment: {{ $comment->id }}
@endsection

@section('content')
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Edit comment: <strong>{{ $comment->id }}</strong></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="{{ route('comment.index') }}">Comments</a></li>
                        <li class="breadcrumb-item active">Edit comment</li>
                    </ol>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <section class="content container-fluid">
        <div class="container-fluid">
            <div class="col-md-12">

                @includeif('partials.errors')

                <div class="card card-default">
                    <div class="card-body">
                        <form method="POST" action="{{ route('comment.update', $comment->id) }}"  role="form" enctype="multipart/form-data">
                            {{ method_field('PATCH') }}
                            @csrf

                            @include('comment.form')

                        </form>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ route('comment.index') }}">Back</a>
        </div>
    </section>
@endsection