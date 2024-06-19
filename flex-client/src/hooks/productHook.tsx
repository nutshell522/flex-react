import { useEffect, useState } from "react";
import { IProductSearchParamReq, ISort, ProductApi } from "../utils/api";

export interface IProductIndex {
    id: number;
    name: string;
    description: string;
    salesPrice: number;
    unitPrice: number;
    productColors: IProductColorIndex[];
}

export interface IProductColorIndex {
    id: number;
    color: string;
    name: string;
    productPictures: IProductPictureIndex[];
}

export interface IProductPictureIndex {
    url: string;
}

export interface IProductSearchParam {
    name?: string;
    maxPrice?: number;
    minPrice?: number;
    topCategoryId?: number;
    middleCategoryId?: number;
    bottomCategoryId?: number;
}

const useProductAPI = () => {
    const [products, setProducts] = useState<IProductIndex[]>([]);
    const [searchParam, setSearchParam] = useState<IProductSearchParam | null>(null);
    const [sort, setSort] = useState<ISort | null>(null);
    const [pageIndex, setPageIndex] = useState<number>(0);
    const PAGE_SIZE = 25;
    const [totalCount, setTotalCount] = useState<number>(0);
    const [hasNextPage, setHasNextPage] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(false);

    const fetchProducts: (searchMode: 'search' | 'append', param: IProductSearchParamReq) => Promise<void> =
        async (searchMode, param) => {
            setLoading(true);
            await new Promise(resolve => setTimeout(resolve, 1500));
            ProductApi.search(param).then((res) => {
                switch (searchMode) {
                    case "search":
                        setProducts(res.data.items);
                        break;
                    case "append":
                        setProducts((prev) => [...prev, ...res.data.items]);
                        break;
                    default:
                        break;
                }
                setTotalCount(res.data.totalCount);
                setHasNextPage(res.data.hasNextPage);
            }).finally(() => {
                setLoading(false);
            });
        };

    useEffect(() => {
        if (!searchParam) return;
        const setRequest = async () => {
            setPageIndex(0);
            setSort(null);
            fetchProducts(
                "search",
                {
                    pageable: {
                        page: 0,
                        size: PAGE_SIZE,
                    },
                    ...searchParam,
                });
        };
        setRequest();
    }, [searchParam]);

    useEffect(() => {
        if (!sort) return;
        setPageIndex(0);
        fetchProducts(
            "search", {
            ...searchParam,
            pageable: {
                page: 0,
                size: PAGE_SIZE,
                sort: sort ? [sort] : undefined,
            },
        });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [sort]);

    useEffect(() => {
        if (pageIndex === 0) return;
        fetchProducts(
            "append", {
            ...searchParam,
            pageable: {
                page: pageIndex,
                size: PAGE_SIZE,
                sort: sort ? [sort] : undefined,
            },
        });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [pageIndex]);


    const nextPage = () => {
        if (!hasNextPage) return;
        setPageIndex((prev) => prev + 1);
    };

    return { products, setSearchParam, nextPage, totalCount, hasNextPage, loading, sort, setSort };
};

export default useProductAPI;