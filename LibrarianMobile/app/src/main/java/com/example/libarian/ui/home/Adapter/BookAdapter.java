package com.example.libarian.ui.home.Adapter;

import android.net.Uri;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Filter;
import android.widget.Filterable;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.libarian.R;
import com.example.libarian.ui.home.Model.Book;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;
import java.util.List;

public class BookAdapter extends RecyclerView.Adapter<BookAdapter.BookViewHolder> implements Filterable {
    public ArrayList<Book> books;
    public clickItem clickItem;

    public ArrayList<Book> booksFilter;

    public BookAdapter(ArrayList<Book> books,clickItem clickItem) {
        this.books = books;
        this.clickItem = clickItem;
        this.booksFilter = books;
    }

    @NonNull
    @Override
    public BookViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.book_item, parent,false);
        return new BookViewHolder(view);

    }

    @Override
    public void onBindViewHolder(@NonNull BookViewHolder holder, int position) {
        Book book = booksFilter.get(position);
        holder.tvName.setText(book.getTitle());

        Picasso.get().load(book.getImage()).into(holder.img);
        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                clickItem.onItemClick(position,book);
            }
        });
    }

    @Override
    public int getItemCount() {
        return booksFilter.size();
    }

    @Override
    public Filter getFilter() {
        return new BookFilter();
    }
    class BookFilter extends Filter{
        @Override
        protected FilterResults performFiltering(CharSequence constraint) {
            String charString = constraint.toString();
            if (charString.isEmpty()){
                booksFilter = books;
            }
            else
            {
                List<Book> filteredList = new ArrayList<>();
                for (Book r: books){
                    if(r.getTitle().toLowerCase().contains(charString.toLowerCase()) ||
                            r.getPublishingYear().contains(constraint) || r.getId().contains(constraint))
                    {
                        filteredList.add(r);
                    }
                }
                booksFilter = (ArrayList<Book>) filteredList;
            }
            FilterResults filterResults = new FilterResults();
            filterResults.values = booksFilter;
            return filterResults;
        }

        @Override
        protected void publishResults(CharSequence charSequence, FilterResults results) {
            booksFilter = (ArrayList<Book>) results.values;
            notifyDataSetChanged();
        }
    }

    class BookViewHolder extends RecyclerView.ViewHolder {
        TextView tvName;
        ImageView img;
        public BookViewHolder(@NonNull View itemView) {
            super(itemView);
            tvName = itemView.findViewById(R.id.tvTitle);
            img = itemView.findViewById(R.id.img);
        }
    }

    public interface clickItem{
        void onItemClick(int pos, Book book);
    }
}
